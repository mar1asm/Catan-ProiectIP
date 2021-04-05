using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatanAPI.Models;

namespace CatanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;

        public UsersController(CatanAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.Select(b => new UserDto
            {
                UserId = b.UserId,
                FirstName = b.FirstName,
                LastName = b.LastName,
                Email = b.Email,
                Notifications = b.UserNotifications
                .Select(
                    n => new NotificationDto { NotificationId = n.UserNotificationId, CreatedAt = n.CreatedAt, Text = n.Notification.Text, Read = n.Read })
                .ToList()
            }).ToListAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context
                .Users.
                Select(entry => 
                new UserDto
                    {
                    UserId = entry.UserId,
                    FirstName = entry.FirstName,
                    LastName = entry.LastName,
                    Email = entry.Email,
                    Notifications = entry.UserNotifications
                    .Select(
                    n => new NotificationDto { NotificationId = n.UserNotificationId, CreatedAt = n.CreatedAt, Text = n.Notification.Text, Read = n.Read })
                    .ToList()
                }
                )
                .SingleOrDefaultAsync(entry => entry.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
