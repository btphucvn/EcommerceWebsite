using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;

namespace EcommerceWebsite.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalsController : ControllerBase
    {
        private readonly dbecommerceContext _context;

        public ArrivalsController(dbecommerceContext context)
        {
            _context = context;
        }

        // GET: api/Arrivals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arrival>>> GetArrivals()
        {
            return await _context.Arrivals.Include(x=>x.Product).OrderBy(x=>x.Sort).ToListAsync();
        }

        // GET: api/Arrivals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arrival>> GetArrival(int id)
        {
            var arrival = await _context.Arrivals.FindAsync(id);

            if (arrival == null)
            {
                return NotFound();
            }

            return arrival;
        }

        //// PUT: api/Arrivals/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutArrival(int id, Arrival arrival)
        //{
        //    if (id != arrival.ProductId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(arrival).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ArrivalExists(id))
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

        //// POST: api/Arrivals
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Arrival>> PostArrival(Arrival arrival)
        //{
        //    _context.Arrivals.Add(arrival);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ArrivalExists(arrival.ProductId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetArrival", new { id = arrival.ProductId }, arrival);
        //}

        //// DELETE: api/Arrivals/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteArrival(int id)
        //{
        //    var arrival = await _context.Arrivals.FindAsync(id);
        //    if (arrival == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Arrivals.Remove(arrival);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ArrivalExists(int id)
        //{
        //    return _context.Arrivals.Any(e => e.ProductId == id);
        //}
    }
}
