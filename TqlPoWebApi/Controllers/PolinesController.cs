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
    public class PolinesController : ControllerBase
    {
        private readonly PoContext _context;

        public PolinesController(PoContext context)
        {
            _context = context;
        }
        //below is method from thur 6-3 homework tuff method
        //recalculatepototal is name we chose for method, pass thru POId , variable name we choose is POID  po id
    //l is var for lines, x is fred var
      
        private async Task RecalculatePoTotal(int POID)
        {
            var po = await _context.POs.FindAsync(POID);
            if (po == null) throw new Exception("FAtal: PO is not found to recalc!");
            var poTotal = (from l in _context.polines
                          join i in _context.Items
                          on l.ItemId equals i.ID
                          where l.POId == POID
                          select new { LineTotal = l.Quantity * i.Price})
                         .Sum( x => x.LineTotal);
            po.Total = poTotal;
            await _context.SaveChangesAsync();
        }

        // GET: api/Polines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poline>>> Getpolines()
        {
            return await _context.polines.ToListAsync();
        }

        // GET: api/Polines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poline>> GetPoline(int id)
        {
            var poline = await _context.polines.FindAsync(id);

            if (poline == null)
            {
                return NotFound();
            }

            return poline;
        }

        // PUT: api/Polines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoline(int id, Poline poline)
        {
            if (id != poline.ID)
            {
                return BadRequest();
            }

            _context.Entry(poline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                //below is entered in for put from method from homework above
                await RecalculatePoTotal(poline.POId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolineExists(id))
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

        // POST: api/Polines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Poline>> PostPoline(Poline poline)
        {
            _context.polines.Add(poline);
            await _context.SaveChangesAsync();
            await RecalculatePoTotal(poline.POId);

            return CreatedAtAction("GetPoline", new { id = poline.ID }, poline);
        }

        // DELETE: api/Polines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Poline>> DeletePoline(int id)
        {
            var poline = await _context.polines.FindAsync(id);
            if (poline == null)
            {
                return NotFound();
            }

            _context.polines.Remove(poline);
            await _context.SaveChangesAsync();
            await RecalculatePoTotal(poline.POId);

            return poline;
        }

        private bool PolineExists(int id)
        {
            return _context.polines.Any(e => e.ID == id);
        }
    }
}
