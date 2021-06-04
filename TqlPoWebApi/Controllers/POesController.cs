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
        //  PUT method for review
        [HttpPut("{id}/review")]
        public async Task<IActionResult> PutPoReviewOrApproved(int id)
        {
            var po = await _context.POs.FindAsync(id);
            if (po == null)
            {
                return NotFound();
            }
            po.Status = (po.Total > 0 && po.Total <= 100 ) ? "APPROVED" : "REVIEW" ;
   //         po.Status = (po.Total > 0 && po.Total <= 100  po.Total = 0) ? "APPROVED" : "REVIEW" : "REJECT";

            return await PutPO(id, po);
        }

        //put method for edit we are updating the Status on PO class
        //the bracket id tells the method which PO we want to update   in postman localhost:53614/api/POes/2/edit goes on the PUT (for this method), GET, Post Delete
        [HttpPut("{id}/edit")]
        public async Task<IActionResult> PutPoToEdit(int id)
        {
            var po = await _context.POs.FindAsync(id);
            if (po == null)
            {
                return NotFound();
            }
            po.Status = "EDIT";
            await _context.SaveChangesAsync();
            return NoContent();


        }
          // PUT method for approve
           [HttpPut("{id}/approve")]   
           public async Task<IActionResult> PutPoToApprove(int id)
           {
        //the (id) in below line is pointing to postman  localhost:53614/api/POes/2/edit
               var po = await _context.POs.FindAsync(id);
               if (po == null)
               {
                   return NotFound();
               }
        // below po is referenecing class var po three lines above
               po.Status = "APPROVE";
               await _context.SaveChangesAsync();
               return NoContent();
            }
        
          // PUT method for reject
           [HttpPut("{id}/reject")]

            public async Task<IActionResult> PutPoToReject(int id)
             {
              var po = await _context.POs.FindAsync(id);
             if (po == null)
             {
                 return NotFound();
             }
              po.Status = "REJECT";
              await _context.SaveChangesAsync();
             return NoContent();
            }


        // GET: api/POes GET ALL THE POS WHERE STATUS IS SET TO REVIEW
        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<PO>>> GetPOsinReview()
        {
            return await _context.POs
                    .Where(p => p.Status == PO.StatusReview)
                    .Include(p => p.Employee)
                    .ToListAsync();
        }
         

        // GET: api/POes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PO>>> GetPOs()
        {
            return await _context.POs
                            .ToListAsync();
        }


        // GET: api/POes/empl
        [HttpGet("empl")]
        public async Task<ActionResult<IEnumerable<PO>>> GetPOsWithEmpl()
        {
            return await _context.POs
                            .Include(x => x.Employee)
                            .ToListAsync();
        }

        // GET: api/POes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PO>> GetPO(int id)
        {
            var pO = await _context.POs
                                .FindAsync(id);

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
