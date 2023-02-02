using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;
using NToastNotify;
using WebShopping.Helpper;
using EcommerceWebsite.Extension;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList.Core;
using Microsoft.AspNetCore.Hosting;

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSlidesController : Controller
    {
        private readonly dbecommerceContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminSlidesController(dbecommerceContext context, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/AdminSlides
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var ls = _context.Slides
                .AsNoTracking()
                .OrderByDescending(x => x.Sort);
            PagedList<Slide> models = new PagedList<Slide>(ls, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminSlides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // GET: Admin/AdminSlides/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminSlides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlideId,Thumb,Link,Sort")] Slide slide, Microsoft.AspNetCore.Http.IFormFile? fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = MD5Tool.CreateMD5(DateTimeTool.GetTimeStampNow().ToString() +slide.Link)+ extension;
                    slide.Thumb = await Utilities.UploadFile(fThumb, @"slides", image.ToLower());
                }
                _context.Add(slide);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tạo thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(slide);
        }

        // GET: Admin/AdminSlides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        // POST: Admin/AdminSlides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SlideId,Thumb,Link,Sort")] Slide slide, Microsoft.AspNetCore.Http.IFormFile? fThumb)
        {
            if (id != slide.SlideId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = MD5Tool.CreateMD5(DateTimeTool.GetTimeStampNow().ToString() + slide.Link) + extension;
                        slide.Thumb = await Utilities.UploadFile(fThumb, @"slides", image.ToLower());
                    }
                    _context.Update(slide);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Cập nhật thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideExists(slide.SlideId))
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
            return View(slide);
        }

        // GET: Admin/AdminSlides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Slides == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // POST: Admin/AdminSlides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Slides == null)
            {
                return Problem("Entity set 'dbecommerceContext.Slides'  is null.");
            }
            var slide = await _context.Slides.FindAsync(id);
            if (slide != null)
            {

                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string path = webRootPath + "\\images\\slides\\" + slide.Thumb;


                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                _context.Slides.Remove(slide);
                _toastNotification.AddSuccessToastMessage("Xóa thành công");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Xóa thất bại");

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideExists(int id)
        {
          return _context.Slides.Any(e => e.SlideId == id);
        }
    }
}
