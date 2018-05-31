using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatsSystem;
using CatsSystem.Data.Entities;

namespace CatsSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/ApiCats")]
    public class ApiCatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiCatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiCats
        [HttpGet]
        public IEnumerable<Cats> GetCats()
        {
            return _context.Cats;
        }

        // GET: api/ApiCats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCats([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cats = await _context.Cats.SingleOrDefaultAsync(m => m.Id == id);

            if (cats == null)
            {
                return NotFound();
            }

            return Ok(cats);
        }

        // PUT: api/ApiCats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCats([FromRoute] int id, [FromBody] Cats cats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cats.Id)
            {
                return BadRequest();
            }

            _context.Entry(cats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatsExists(id))
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

        // POST: api/ApiCats
        [HttpPost]
        public async Task<IActionResult> PostCats([FromBody] Cats cats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cats.Add(cats);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCats", new { id = cats.Id }, cats);
        }

        // DELETE: api/ApiCats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCats([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cats = await _context.Cats.SingleOrDefaultAsync(m => m.Id == id);
            if (cats == null)
            {
                return NotFound();
            }

            _context.Cats.Remove(cats);
            await _context.SaveChangesAsync();

            return Ok(cats);
        }

        private bool CatsExists(int id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }
}