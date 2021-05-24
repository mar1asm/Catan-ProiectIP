using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CatanAPI.Data.DTO.Messages;
using CatanAPI.Data;
using CatanAPI.Models;
using CatanAPI.Data.DTO.GameSessionsDTO;
using CatanAPI.Data.DTO.ExtensionsDTO;
using CatanAPI.Data.DTO.UsersDTO;
using System;

namespace CatanAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionsController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;
        private readonly UserManager<User> _userManager;

        public DateTime Datetime { get; private set; }

        public GameSessionsController(CatanAPIDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        // GET: api/GameSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetSessionMinDto>>> GetGameSessions()
        {
            var sessions = await _context.GameSessions.Select(session => new GetSessionMinDto
            {
                Id = session.Id,
                CreatedAt = session.CreatedAt,
                Status = session.Status

            }).ToListAsync();
            return Ok(sessions);
        }

        // GET: api/GameSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSessionDto>> GetGameSession(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var gameSession = await _context.GameSessions
                .Include(i => i.GameSessionUsers)
                .ThenInclude(gUser => gUser.User)
                .Include(i => i.Extensions)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (gameSession == null || gameSession.GameSessionUsers == null)
            {
                return NotFound();
            }
            if (!gameSession.GameSessionUsers.Any(item => item.UserId == currentUser.Id))
            {
                return Unauthorized();
            }

            return new GetSessionDto {
                Id = gameSession.Id,
                CreatedAt = gameSession.CreatedAt,
                Status = gameSession.Status,
                Extensions = gameSession.Extensions.Select(extension => new GetExtensionDTO { Id = extension.Id, Name = extension.Name }).ToList(),
                GameSessionUsers = gameSession.GameSessionUsers.Select(user => new GetUserSessionDTO { Id = user.UserId, UserName = user.User.UserName, Roles = user.SessionRoles }).ToList()
            };
        }

        // GET: api/GameSession/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<GetSessionDto>>> GetMySessions()
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var gameSessions = await _context.GameSessions
                .Include(i => i.GameSessionUsers)
                .Where(item => item.GameSessionUsers.Any(user => user.UserId == currentUser.Id))
                .Select(gameSession => new GetSessionDto
                {
                    Id = gameSession.Id,
                    CreatedAt = gameSession.CreatedAt,
                    Status = gameSession.Status,
                    Extensions = gameSession.Extensions.Select(extension => new GetExtensionDTO { Id = extension.Id, Name = extension.Name }).ToList(),
                    GameSessionUsers = gameSession.GameSessionUsers.Select(user => new GetUserSessionDTO { Id = user.UserId, UserName = user.User.UserName, Roles=user.SessionRoles }).ToList()
                }).ToListAsync();
            return gameSessions;
        }

        [HttpPost("{id}/user")]
        public async Task<ActionResult<GetSessionMinDto>> AddUser(int id, AddUserToSessionDTO userInvite)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userInvite.UserName);
            var session = await _context.GameSessions
                .Include(s => s.GameSessionUsers)
                .FirstOrDefaultAsync(entry => entry.Id == id);
            if (session == null || session.GameSessionUsers == null || targetUser == null)
            {
                return NotFound();
            }
            if (!session.GameSessionUsers.Any(sessionUser => sessionUser.UserId == currentUser.Id 
                && (sessionUser.SessionRoles & GameSessionRoles.GameAdmin) > 0))
            {
                return Unauthorized();
            }
            if(session.GameSessionUsers.Any(sessionUser => sessionUser.UserId == targetUser.Id))
            {
                return BadRequest();
            }

            session.GameSessionUsers.Add(new GameSessionUser
            {
                GameSession = session,
                GameSessionId = session.Id,
                SessionRoles = GameSessionRoles.GameUser,
                Status = GameSessionUserStatus.Pending,
                User = targetUser,
                UserId = targetUser.Id
            });
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGameSession", new { id = session.Id }, new GetSessionMinDto
            {
                Id = session.Id,
                CreatedAt = session.CreatedAt,
                Status = session.Status
            });
        }

        [HttpPut("{id}/user")]
        public async Task<IActionResult> PutSessionInviteStatus(int id, SetSessionStatusRequest status)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var sessionUserEntry = await _context.GameSessionUsers
                .Where(sUser => sUser.UserId == currentUser.Id && sUser.GameSessionId == id).FirstOrDefaultAsync();
            if(sessionUserEntry == null)
            {
                return NotFound();
            }
            if(status.Status != GameSessionUserStatus.Accepted && status.Status != GameSessionUserStatus.Declined)
            {
                return BadRequest();
            }
            sessionUserEntry.Status = status.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GameSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetSessionMinDto>> PostGameSession(CreateSessionDto gameSession)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var session = new GameSession
            {
                CreatedAt = DateTime.Now,
                Status = GameSessionStatus.Pending,
                Extensions = _context.Extensions
                .Where(item => gameSession.Extensions.Any(extensionItem => extensionItem == item.Id)).ToList(),
                GameSessionUsers = new List<GameSessionUser>(),
                GameSessionMessages = new List<GameSessionMessage>()
            };
            _context.GameSessions.Add(session);
            await _context.SaveChangesAsync();
            session.GameSessionUsers.Add(new GameSessionUser
            {
                GameSession = session,
                GameSessionId = session.Id,
                SessionRoles = GameSessionRoles.GameAdmin,
                Status = GameSessionUserStatus.Accepted,
                User = currentUser,
                UserId = currentUser.Id
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameSession", new { id = session.Id }, new GetSessionMinDto
            {
                Id = session.Id,
                CreatedAt = session.CreatedAt,
                Status = session.Status
            });
        }

        // DELETE: api/GameSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSession(int id)
        {
            var gameSession = await _context.GameSessions.FindAsync(id);
            if (gameSession == null)
            {
                return NotFound();
            }

            _context.GameSessions.Remove(gameSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Route("{id}/messages")]
        [HttpGet]
        public async Task<IActionResult> GetSessionMessages(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var gameSession = await _context.GameSessions
                .Include(i => i.GameSessionUsers)
                .Include(i => i.Extensions)
                .Include(i => i.GameSessionMessages)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (gameSession == null || gameSession.GameSessionUsers == null)
            {
                return NotFound();
            }
            if (!gameSession.GameSessionUsers.Any(item => item.UserId == currentUser.Id))
            {
                return Unauthorized();
            }

            var sessionMessages = gameSession.GameSessionMessages.Select(t => new GameSessionMessageDto
            {
                Id = t.Id,
                Message = t.Message,
                Date = t.Date,
                FromUserName = t.From.UserName
            }).ToList();

            return Ok(sessionMessages);
        }

        [Route("{id}/messages")]
        [HttpPost]
        public async Task<IActionResult> PostSessionMessage(int id, GameSessionMessageFormDto newMessageDto)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var gameSession = await _context.GameSessions
                .Include(i => i.GameSessionUsers)
                .Include(i => i.Extensions)
                .Include(i => i.GameSessionMessages)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (gameSession == null || gameSession.GameSessionUsers == null)
            {
                return NotFound();
            }
            if (!gameSession.GameSessionUsers.Any(item => item.UserId == currentUser.Id))
            {
                return Unauthorized();
            }

            var newMessage = new GameSessionMessage
            {
                Message = newMessageDto.Message,
                Date = DateTime.Now,
                FromId = currentUser.Id,
                From = currentUser,
                GameSessionId = gameSession.Id,
                GameSession = gameSession
            };

            gameSession.GameSessionMessages.Add(newMessage);

            await _context.SaveChangesAsync();

            return Ok(new GameSessionMessageDto
            {
                Id = newMessage.Id,
                Message = newMessage.Message,
                Date = newMessage.Date,
                FromUserName = newMessage.From.UserName
            });
        }

        private bool GameSessionExists(int id)
        {
            return _context.GameSessions.Any(e => e.Id == id);
        }
    }
}
