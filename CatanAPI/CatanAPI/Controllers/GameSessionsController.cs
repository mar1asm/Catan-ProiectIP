using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatanAPI.Data;
using CatanAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CatanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionsController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;
        private readonly UserManager<User> _userManager;

        public GameSessionsController(CatanAPIDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        // GET: api/GameSessions
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSession>>> GetGameSessions()
        {
            return await _context.GameSessions.ToListAsync();
        }

        // GET: api/GameSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSession>> GetGameSession(int id)
        {
            var gameSession = await _context.GameSessions.FindAsync(id);

            if (gameSession == null)
            {
                return NotFound();
            }

            return gameSession;
        }

        // PUT: api/GameSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameSession(int id, GameSession gameSession)
        {
            if (id != gameSession.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameSession>> PostGameSession(GameSession gameSession)
        {
            _context.GameSessions.Add(gameSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameSession", new { id = gameSession.Id }, gameSession);
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
