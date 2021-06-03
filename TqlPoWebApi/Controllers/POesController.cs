using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TqlPoWebApi.Data;
using TqlPoWebApi.Models;

namespace TqlPoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POesController : ControllerBase
    {
        private readonly PoContext _context;

        public POesController(PoContext context)
        {
            _context = context;
        }

        // GET: api/POes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PO>>> GetPOs()
        {
            return await _context.POs
                            .Include(x=> x.Employee)
                            .ToListAsync();
        }

        // GET: api/POes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PO>> GetPO(int id)
        {
            var pO = await _context.POs.FindAsync(id);

            if (pO == null)
            {
                return NotFound();
            }

            return pO;
        }

        // PUT: api/POes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPO(int id, PO pO)
        {
            if (id != pO.ID)
            {
                return BadRequest();
            }

            _context.Entry(pO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POExists(id))
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

        // POST: api/POes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PO>> PostPO(PO pO)
        {
            _context.POs.Add(pO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPO", new { id = pO.ID }, pO);
        }

        // DELETE: api/POes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PO>> DeletePO(int id)
        {
            var pO = await _context.POs.FindAsync(id);
            if (pO == null)
            {
                return NotFound();
            }

            _context.POs.Remove(pO);
            await _context.SaveChangesAsync();

            return pO;
        }

        private bool POExists(int id)
        {
            return _context.POs.Any(e => e.ID == id);
        }
    }
}
