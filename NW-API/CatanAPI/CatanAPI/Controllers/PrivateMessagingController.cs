using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatanAPI.Data;
using CatanAPI.Models;
using CatanAPI.Data.DTO.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CatanAPI.Models.Authentication;

namespace CatanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateMessagingController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;
        private readonly UserManager<User> _userManager;

        public PrivateMessagingController(CatanAPIDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        // GET: api/PrivateMessaging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrivateMessageDto>>> GetMessage()
        {
            var messages = await _context.PrivateMessages.Select(t => new PrivateMessageDto
            {
                Id = t.Id,
                FromUserName = t.From.UserName,
                ToUserName = t.To.UserName,
                Message = t.Message,
                Date = t.Date
            }).ToListAsync();
            return Ok(messages);
        }

        //GET : api/PrivateMessaging
        [Authorize]
        [HttpGet("{fromUserName}")]
        [ProducesResponseType(typeof(PrivateMessageDto), 201)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMessage(string fromUserName)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var fromUser = await _userManager.FindByNameAsync(fromUserName);
            if (fromUser == null)
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Error", Message = "Requested user doesn't exist." });
            if (fromUser == currentUser)
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Error", Message = "Can't view messages sent to self." });
            var messages = await _context.PrivateMessages.Select(t => new PrivateMessageDto
            {
                Id = t.Id,
                FromUserName = t.From.UserName,
                ToUserName = t.To.UserName,
                Message = t.Message,
                Date = t.Date
            }).Where(t => t.FromUserName == fromUserName && t.ToUserName == currentUser.UserName).ToListAsync();
            return Ok(messages);
        }


        //// GET: api/PrivateMessaging/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PrivateMessage>> GetMessage(int id)
        //{
        //    var message = await _context.Message.FindAsync(id);

        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return message;
        //}

        //// PUT: api/PrivateMessaging/5 -- Not Needed
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMessage(int id, PrivateMessage message)
        //{
        //    if (id != message.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MessageExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/PrivateMessaging
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(PrivateMessageDto), 201)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //ActionResult<TextSendDto>
        public async Task<IActionResult> PostMessage(PrivateMessageFormDto message)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var toUser = await _userManager.FindByNameAsync(message.ToUserName);
            if (toUser == null)
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Error", Message = "Requested user doesn't exist." });
            if (toUser == currentUser)
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Error", Message = "Can't send message to oneself." });
            var newText = new PrivateMessage
            {
                FromId = currentUser.Id,
                From = currentUser,
                ToId = toUser.Id,
                To = toUser,
                Message = message.Message,
                Date = DateTime.Now

            };
            _context.PrivateMessages.Add(newText);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostMessage", new { id = newText.Id }, new PrivateMessageDto { Id = newText.Id, FromUserName = newText.From.UserName, ToUserName = newText.To.UserName, Message = newText.Message, Date = newText.Date });
        }

        //// DELETE: api/PrivateMessaging/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMessage(int id)
        //{
        //    var message = await _context.Message.FindAsync(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Message.Remove(message);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool MessageExists(int id)
        {
            return _context.PrivateMessages.Any(e => e.Id == id);
        }
    }
}