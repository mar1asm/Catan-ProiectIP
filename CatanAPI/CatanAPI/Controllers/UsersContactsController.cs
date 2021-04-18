using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatanAPI.Data;
using CatanAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CatanAPI.Data.DTO;

namespace CatanAPI.Controllers
{
    [Route("api/user/contacts")]
    [ApiController]
    public class UsersContactsController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersContactsController(CatanAPIDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var contacts = await _context.Contacts.Where(item => item.Sender.Id == user.Id || item.Receiver.Id == user.Id)
                .Select(c => new ContactDto
            {
                Id = c.Id,
                UserName = c.Sender.Id == user.Id ? c.Receiver.UserName : c.Sender.UserName
            }).ToListAsync();
            return contacts;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (contact == null)
            {
                return NotFound();
            }

            return new ContactDto
            {
                Id = contact.Id,
                Accepted = contact.Accepted,
                UserName = user.Id == contact.Sender.Id ? contact.Receiver.UserName : contact.Sender.UserName
            };
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, ContactDto contact)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var contactEntry = await _context.Contacts.SingleOrDefaultAsync(entry => entry.Id == id);
            if(contactEntry == null)
            {
                return NotFound();
            }
            // We can only accept requests from others, not force accept requests initiated by us
            if((contactEntry.ReceiverId != currentUser.Id && contactEntry.SenderId != currentUser.Id) || contactEntry.ReceiverId != currentUser.Id)
            {
                return Unauthorized();
            }
            contactEntry.Accepted = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ContactDto>> PostContact(AddContactDto contact)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var other = await _userManager.FindByNameAsync(contact.UserName);
            if(other == null)
            {
                return NotFound();
            }
            var newContact = new Contact
            {
                SenderId = user.Id,
                Sender = user,
                ReceiverId = other.Id,
                Receiver = other,
                Accepted = false
            };
            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { Id = newContact.Id}, new ContactDto { Id = newContact.Id, UserName = other.UserName });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
