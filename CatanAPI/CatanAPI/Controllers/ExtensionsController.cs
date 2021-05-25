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
using Newtonsoft.Json;
using CatanAPI.Data.DTO.ExtensionsDTO;

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
        public async Task<ActionResult<IEnumerable<GetExtensionDTO>>> GetExtensions()
        {
            return await _context.Extensions.Select(extension => new GetExtensionDTO {
                Id = extension.Id,
                Name = extension.Name,
            }).ToListAsync();
        }

        // GET: api/Extensions/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetExtensionFullDTO>> GetExtension(int id)
        {
            var extension = await _context.Extensions.FindAsync(id);

            if (extension == null)
            {
                return NotFound();
            }

            return new GetExtensionFullDTO
            {
                Id = extension.Id,
                Name = extension.Name,
                Data = extension.Data
            };
        }

        // PUT: api/Extensions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExtension(int id, CreateExtensionDTO extension)
        {
            var extensionData = await _context.Extensions.FindAsync(id);
            try
            {
                JsonConvert.DeserializeObject(extension.Data);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            extensionData.Data = extension.Data;

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
        public async Task<ActionResult<GetExtensionFullDTO>> PostExtension(CreateExtensionDTO extension)
        {
            try
            {
                JsonConvert.DeserializeObject(extension.Data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            var ext = new Extension
            {
                Name = extension.Name,
                Data = extension.Data
            };
            _context.Extensions.Add(ext);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExtension", new { id = ext.Id }, new GetExtensionFullDTO {
                Id = ext.Id,
                Data = ext.Data,
                Name = ext.Name
            });
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
