using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitorCounterAPI.Data;
using VisitorCounterAPI.Models;

namespace VisitorCounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly VisitorCounterAPIContext _context;

        public VisitorsController(VisitorCounterAPIContext context)
        {
            _context = context;
        }

        // GET: api/Visitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitor()
        {
            return await _context.Visitor.ToListAsync();
        }

        // GET: api/Visitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(Guid id)
        {
            var visitor = await _context.Visitor.FindAsync(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        // PUT: api/Visitors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitor(Guid id, SubmitModel submit)
        {
            if (!submit.Increment)
            {
                return BadRequest();
            }
            var visitor = await _context.Visitor.FindAsync(id);
            if (visitor == null)
            {
                visitor = new() { Id = id, Visits = 1 };
                _context.Visitor.Add(visitor);
            }
            else
            {
                _context.Entry(visitor).State = EntityState.Modified;
                visitor.Visits++;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorExists(id))
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

        // POST: api/Visitors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitor(Visitor visitor)
        {
            _context.Visitor.Add(visitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, visitor);
        }

        // DELETE: api/Visitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(Guid id)
        {
            var visitor = await _context.Visitor.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }

            _context.Visitor.Remove(visitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitorExists(Guid id)
        {
            return _context.Visitor.Any(e => e.Id == id);
        }
    }
}
