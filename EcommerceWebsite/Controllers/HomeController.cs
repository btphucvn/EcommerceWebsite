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
            dynamic model = new System.Dynamic.ExpandoObject();

            string categoriesJson = await RestAPI.GetJSON("https://"+Request.Host.Value+"/api/Categories");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            string arrivalJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/arrivals");
            List<Arrival> arrival = JsonConvert.DeserializeObject<List<Arrival>>(arrivalJson);

            string slideJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/slides");
            List<Slide> slide = JsonConvert.DeserializeObject<List<Slide>>(slideJson);

            model.Categories = categories;
            model.Arrivals = arrival;
            model.Slides = slide;

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