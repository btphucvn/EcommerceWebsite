using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;
using NToastNotify;
using PagedList.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminArrivalsController : Controller
    {
        private readonly dbecommerceContext _context;
        private readonly IToastNotification _toastNotification;

        public AdminArrivalsController(dbecommerceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: Admin/AdminArrivals
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var ls = _context.Arrivals
                .AsNoTracking()
                .Include(x => x.Product)
                .OrderByDescending(x => x.Sort);
            PagedList<Arrival> models = new PagedList<Arrival>(ls, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        // GET: Admin/AdminArrivals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Arrivals == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrivals
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (arrival == null)
            {
                return NotFound();
            }

            return View(arrival);
        }

        // GET: Admin/AdminArrivals/Create
        public IActionResult Create()
        {
            var selectListItems = from product in _context.Products
                                  where !_context.Arrivals.Any(arrival => arrival.ProductId == product.ProductId)
                                  select product;
            ViewData["Product"] = new SelectList(selectListItems, "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/AdminArrivals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Sort")] Arrival arrival)
        {
            var message = string.Join(" | ", ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage));
            arrival.Product = null;
            if (ModelState.IsValid)
            {
                _context.Add(arrival);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tạo thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", arrival.ProductId);
            return View(arrival);
        }

        // GET: Admin/AdminArrivals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Arrivals == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrivals.FindAsync(id);
            if (arrival == null)
            {
                return NotFound();
            }
            ViewData["Product"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View(arrival);
        }

        // POST: Admin/AdminArrivals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Sort")] Arrival arrival)
        {
            if (id != arrival.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arrival);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Sửa thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArrivalExists(arrival.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", arrival.ProductId);
            return View(arrival);
        }

        // GET: Admin/AdminArrivals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Arrivals == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrivals
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (arrival == null)
            {
                return NotFound();
            }

            return View(arrival);
        }

        // POST: Admin/AdminArrivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Arrivals == null)
            {
                return Problem("Entity set 'dbecommerceContext.Arrivals'  is null.");
            }
            var arrival = await _context.Arrivals.FindAsync(id);
            if (arrival != null)
            {
                _context.Arrivals.Remove(arrival);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Xóa thành công");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Xóa thất bại");

            }
            return RedirectToAction(nameof(Index));
        }

        private bool ArrivalExists(int id)
        {
          return _context.Arrivals.Any(e => e.ProductId == id);
        }
    }
}
