using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList.Core;
using NToastNotify;
using WebShopping.Helpper;

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPostsController : Controller
    {
        private readonly dbecommerceContext _context;
        private readonly IToastNotification _toastNotification;
        public AdminPostsController(dbecommerceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: Admin/AdminPosts
        public async Task<IActionResult> Index(int? page)
        {

            //var tin = _context.Posts.Find(1);
            //for(int i = 1; i < 20; i++)
            //{
            //    Post post = new Post();
            //    post.Title = tin.Title;
            //    post.Published= tin.Published;
            //    post.Alias= tin.Alias;
            //    post.IsHot= tin.IsHot;
            //    post.IsNewfeed= tin.IsNewfeed;
            //    post.CreatedDate= tin.CreatedDate;
            //    post.MetaDesc= tin.MetaDesc;
            //    post.MetaKey= tin.MetaKey;
            //    _context.Add(post);
            //    _context.SaveChanges();
            //}

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var lsPosts = _context.Posts
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<Post> models = new PagedList<Post>(lsPosts, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/AdminPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(post.Title) + extension;
                    post.Thumb = await Utilities.UploadFile(fThumb, @"posts", image.ToLower());
                }
                post.Alias = Utilities.SEOUrl(post.Title);
                _context.Add(post);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tạo thành công");

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Admin/AdminPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Admin/AdminPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] Post post, Microsoft.AspNetCore.Http.IFormFile? fThumb)
        {
            if (id != post.PostId)
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
                        string image = Utilities.SEOUrl(post.Title) + extension;
                        post.Thumb = await Utilities.UploadFile(fThumb, @"posts", image.ToLower());
                    }
                    post.Alias = Utilities.SEOUrl(post.Title);
                    _context.Update(post);

                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Cập nhật thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            return View(post);
        }

        // GET: Admin/AdminPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/AdminPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'dbecommerceContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Xóa thành công");

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
