using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;
using PagedList.Core;
using NToastNotify;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebShopping.Helpper;

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly dbecommerceContext _context;
        private readonly IToastNotification _toastNotification;

        public AdminCategoriesController(dbecommerceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }

        // GET: Admin/AdminCategories
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var ls = _context.Categories
                .AsNoTracking()
                .OrderByDescending(x => x.CatId);
            PagedList<Category> models = new PagedList<Category>(ls, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(x => x.Parent)
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");

            return View();
        }

        // POST: Admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Category category, Microsoft.AspNetCore.Http.IFormFile? fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(category.CatName) + extension;
                    category.Thumb = await Utilities.UploadFile(fThumb, @"categories", image.ToLower());
                }
                category.Alias = Utilities.SEOUrl(category.CatName);
                _context.Add(category);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tạo thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");

            return View(category);
        }

        // POST: Admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Category category, Microsoft.AspNetCore.Http.IFormFile? fThumb)
        {
            if (id != category.CatId)
            {
                return NotFound();
            }
            var message = string.Join(" | ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage));
            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(category.CatName) + extension;
                        category.Thumb = await Utilities.UploadFile(fThumb, @"categories", image.ToLower());
                    }
                    category.Alias = Utilities.SEOUrl(category.CatName);
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Cập nhật thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'dbecommerceContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CatId == id);
        }
    }
}
