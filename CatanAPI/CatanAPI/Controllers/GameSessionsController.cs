using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
                .Include(i => i.Extensions)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (gameSession == null || gameSession.GameSessionUsers == null)
            {
                return NotFound();
            }
            if(!gameSession.GameSessionUsers.Any(item => item.User.Id == currentUser.Id))
            {
                return Unauthorized();
            }

            return new GetSessionDto { 
                Id = gameSession.Id,
                CreatedAt = gameSession.CreatedAt,
                Status = gameSession.Status,
                Extensions = gameSession.Extensions.Select(extension => new GetExtensionDTO { Id = extension.Id, Name = extension.Name}).ToList(),
                GameSessionUsers = gameSession.GameSessionUsers.Select(user => new GetUserMinDTO { Id = user.UserId, UserName= user.User.UserName }).ToList()
            };
        }

        // POST: api/GameSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameSession>> PostGameSession(CreateSessionDto gameSession)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var session = new GameSession
            {
                CreatedAt = DateTime.Now,
                Status = GameSessionStatus.Pending,
                Extensions = _context.Extensions
                .Where(item => gameSession.Extensions.Any(extensionItem => extensionItem == item.Id)).ToList(),
                GameSessionUsers = new List<GameSessionUser>()
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

            return CreatedAtAction("GetGameSession", new { id = session.Id }, session);
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

        private bool GameSessionExists(int id)
        {
            return _context.GameSessions.Any(e => e.Id == id);
        }
    }
}
