using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using CatanAPI.Models;
using CatanAPI.Data;
using CatanAPI.Data.DTO.NotificationsDTO;
using CatanAPI.Data.DTO.UsersDTO;

namespace CatanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(CatanAPIDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsers()
        {
            var users = await _context.Users.Select(b => new GetUserDTO
            {
                Id = b.Id,
                FirstName = b.FirstName,
                LastName = b.LastName,
                IconPath = b.IconPath,
                Level = b.Level,
                Email = b.Email,
                UserName = b.UserName,
                Notifications = b.UserNotifications
                .Select(
                    n => new NotificationDto { NotificationId = n.Id, CreatedAt = n.CreatedAt, Text = n.Notification.Text, Read = n.Read })
                .ToList()
            }).ToListAsync();
            return Ok(users);
        }

        // Get: api/Users/me
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<GetUserDTO>> GetLoggedInUser()
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var user = await _context
                .Users.
                Select(entry =>
                new GetUserDTO
                {
                    Id = entry.Id,
                    FirstName = entry.FirstName,
                    LastName = entry.LastName,
                    UserName = entry.UserName,
                    IconPath = entry.IconPath,
                    Level = entry.Level,
                    Email = entry.Email,
                    Notifications = entry.UserNotifications
                    .Select(
                    n => new NotificationDto { NotificationId = n.Id, CreatedAt = n.CreatedAt, Text = n.Notification.Text, Read = n.Read })
                    .ToList()
                }
                )
                .SingleOrDefaultAsync(entry => entry.Id == currentUser.Id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetUser(string id)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if(id != currentUser.Id)
            {
                return Unauthorized();
            }
            var user = await _context
                .Users.
                Select(entry => 
                new GetUserDTO
                    {
                    Id = entry.Id,
                    FirstName = entry.FirstName,
                    LastName = entry.LastName,
                    UserName = entry.UserName,
                    IconPath = entry.IconPath,
                    Level = entry.Level,
                    Email = entry.Email,
                    Notifications = entry.UserNotifications
                    .Select(
                    n => new NotificationDto { NotificationId = n.Id, CreatedAt = n.CreatedAt, Text = n.Notification.Text, Read = n.Read })
                    .ToList()
                }
                )
                .SingleOrDefaultAsync(entry => entry.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserUpdateDto user)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (id != currentUser.Id)
            {
                return Unauthorized();
            }
            var userEntry = await _context.Users.SingleOrDefaultAsync(entry => entry.Id == id);
            if(userEntry == null)
            {
                return NotFound();
            }
            userEntry.FirstName = user.FirstName ?? userEntry.FirstName;
            userEntry.LastName = user.LastName ?? userEntry.LastName;
            userEntry.Email = user.Email ?? userEntry.Email;
            userEntry.UserName = user.UserName ?? userEntry.UserName;
            userEntry.IconPath = user.IconPath ?? userEntry.IconPath;
            userEntry.Level = user.Level ?? userEntry.Level;

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

        // PUT: api/Users/me
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> PutLoggedInUser(UserUpdateDto user)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var userEntry = await _context.Users.SingleOrDefaultAsync(entry => entry.Id == currentUser.Id);
            if (userEntry == null)
            {
                return NotFound();
            }
            userEntry.FirstName = user.FirstName ?? userEntry.FirstName;
            userEntry.LastName = user.LastName ?? userEntry.LastName;
            userEntry.Email = user.Email ?? userEntry.Email;
            userEntry.UserName = user.UserName ?? userEntry.UserName;
            userEntry.IconPath = user.IconPath ?? userEntry.IconPath;
            userEntry.Level = user.Level ?? userEntry.Level;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(currentUser.Id))
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

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
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

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}