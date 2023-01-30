using EcommerceWebsite.Helpper;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EcommerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string categoriesJson = await RestAPI.GetJSON("https://localhost:44335/api/Categories");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);
            dynamic model = new System.Dynamic.ExpandoObject();
            model.Categories = categories;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}