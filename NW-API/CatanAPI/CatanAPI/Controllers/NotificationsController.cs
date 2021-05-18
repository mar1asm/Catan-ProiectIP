﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanAPI.Data;
using CatanAPI.Data.DTO.NotificationsDTO;
using CatanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatanAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;

        public NotificationsController(CatanAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotifications()
        {
            var sessions = await _context.Notifications.Select(notification => new NotificationDto
            {
                NotificationId = notification.Id,
                CreatedAt = notification.CreatedAt,
                Text = notification.Text,
                Read = notification.UserNotifications.Where(x => x.Read.Equals(false)).ToList().Count.Equals(0) ? false : true
            }).ToListAsync();
            return Ok(sessions);
        }

        // GET api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotificationById(int id)
        {
            var notification = await _context.Notifications
                .Include(i => i.UserNotifications)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (notification == null)
            {
                return NotFound();
            }

            return new NotificationDto
            {
                NotificationId = notification.Id,
                CreatedAt = notification.CreatedAt,
                Text = notification.Text,
                Read = notification.UserNotifications.Where(x => x.Read.Equals(false)).ToList().Count.Equals(0) ? false : true
            };
        }

        // POST api/Notifications
        [HttpPost]
        public async Task<ActionResult<NotificationDto>> PostNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = notification.Id }, notification);
        }

        // PUT api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, NotificationDto notificationDto)
        {
            var notificationEntry = await _context.Notifications.SingleOrDefaultAsync(entry => entry.Id == id);

            if (notificationEntry == null)
            {
                return NotFound();
            }

            notificationEntry.CreatedAt = notificationDto.CreatedAt;
            notificationEntry.Text = notificationDto.Text;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // DELETE api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if(notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
