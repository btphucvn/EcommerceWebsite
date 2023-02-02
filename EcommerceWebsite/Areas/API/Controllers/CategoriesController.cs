using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;
using Newtonsoft.Json;

namespace EcommerceWebsite.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly dbecommerceContext _context;

        public CategoriesController(dbecommerceContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];
                categories[i].Products.Clear();
                if (category.Levels != 1)
                {
                    categories.Remove(category);
                    i--;
                }
                if (i < 0)
                {
                    i = 0;
                }

            }
            return categories;

        }

        // GET: api/products
        [HttpGet("GetProductByAlias/{alias}")]
        public async Task<ActionResult<Category>> GetProductByAlias(string alias)
        {
            var category = await _context.Categories
                .Include(x => x.Products)
                .Where(x => x.Alias == alias)
                .FirstAsync();
            return category;
        }

        // GET: api/Categories/5
        [HttpGet("{alias}")]
        public async Task<ActionResult<Category>> GetCategory(string alias)
        {
            var category = await _context.Categories
                .Include(x=>x.Products)
                .Where(x => x.Alias == alias)
                .FirstAsync();

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        //// PUT: api/Categories/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        //{
        //    if (id != category.CatId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
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

        //// POST: api/Categories
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Category>> PostCategory(Category category)
        //{
        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCategory", new { id = category.CatId }, category);
        //}

        //// DELETE: api/Categories/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var category = await _context.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Categories.Remove(category);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CategoryExists(int id)
        //{
        //    return _context.Categories.Any(e => e.CatId == id);
        //}
    }
}
