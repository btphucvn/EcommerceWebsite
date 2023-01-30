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

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminColorsController : Controller
    {
        private readonly dbecommerceContext _context;
        private readonly IToastNotification _toastNotification;

        public AdminColorsController(dbecommerceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }

        // GET: Admin/AdminColors
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var ls = _context.Colors
                .AsNoTracking()
                .OrderByDescending(x => x.ColorId);
            PagedList<Color> models = new PagedList<Color>(ls, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        // GET: Admin/AdminColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Admin/AdminColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorId,ColorCode")] Color color)
        {
            if (ModelState.IsValid)
            {
                _context.Add(color);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tạo thành công");

                return RedirectToAction(nameof(Index));

            }
            return View(color);
        }

        // GET: Admin/AdminColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Admin/AdminColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorId,ColorCode")] Color color)
        {
            if (id != color.ColorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Sửa thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.ColorId))
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
            return View(color);
        }

        // GET: Admin/AdminColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Admin/AdminColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Colors == null)
            {
                return Problem("Entity set 'dbecommerceContext.Colors'  is null.");
            }
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Xóa thành công");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Xóa thất bại");

            }

            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(int id)
        {
          return _context.Colors.Any(e => e.ColorId == id);
        }
    }
}
