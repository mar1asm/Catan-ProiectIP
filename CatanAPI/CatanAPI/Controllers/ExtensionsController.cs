using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatanAPI.Data;
using CatanAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace CatanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionsController : ControllerBase
    {
        private readonly CatanAPIDbContext _context;

        public ExtensionsController(CatanAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/Extensions
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Extension>>> GetExtensions()
        {
            return await _context.Extensions.ToListAsync();
        }

        // GET: api/Extensions/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Extension>> GetExtension(int id)
        {
            var extension = await _context.Extensions.FindAsync(id);

            if (extension == null)
            {
                return NotFound();
            }

            return extension;
        }

        // PUT: api/Extensions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExtension(int id, Extension extension)
        {
            if (id != extension.Id)
            {
                return BadRequest();
            }

            _context.Entry(extension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtensionExists(id))
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

        // POST: api/Extensions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Extension>> PostExtension(Extension extension)
        {
            _context.Extensions.Add(extension);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExtension", new { id = extension.Id }, extension);
        }

        // DELETE: api/Extensions/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExtension(int id)
        {
            var extension = await _context.Extensions.FindAsync(id);
            if (extension == null)
            {
                return NotFound();
            }

            _context.Extensions.Remove(extension);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExtensionExists(int id)
        {
            return _context.Extensions.Any(e => e.Id == id);
        }
    }
}
