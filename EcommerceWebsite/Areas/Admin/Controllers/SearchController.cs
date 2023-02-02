using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebShopping.Helpper;

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly dbecommerceContext _context;

        public SearchController(dbecommerceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindCustomer(string keyword)
        {
            List<Customer> ls = new List<Customer>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListCustomersSearchPartial", null);
            }
            if (Utilities.IsInteger(keyword))
            {
                ls = _context.Customers
                    .AsNoTracking()
                    .Where(x => x.Phone.Contains(keyword))
                    .Take(10)
                    .OrderByDescending(x => x.CustomerId).ToList();
                
            }
            else
            {
                ls = _context.Customers
                    .AsNoTracking()
                    .Where(x => x.FullName
                    .Contains(keyword) || x.Email.Contains(keyword))
                    .OrderByDescending(x => x.CustomerId)
                    .Take(10)
                    .ToList();
            }
            if (ls != null)
            {
                return PartialView("ListCustomersSearchPartial", ls);
            }
            else
            {
                return PartialView("ListCustomersSearchPartial", null);
            }
        }        
        
        [HttpPost]
        public async Task<IActionResult> FindProduct(string keyword)
        {
            List<Product> ls = new List<Product>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            ls = _context.Products
                    .AsNoTracking()
                    .Include(a=>a.Cat)
                    .Where(x => x.ProductName.Contains(keyword))
                    .Take(10)
                    .OrderByDescending(x => x.ProductName)
                    .ToList();

            if (ls != null)
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", null);
            }
        }
    }
}
