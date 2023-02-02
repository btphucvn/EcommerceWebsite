using EcommerceWebsite.Helpper;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceWebsite.Controllers
{
    [Route("danh-muc")]
    public class CategoriesController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public CategoriesController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index(string? alias)
        {
            dynamic model = new System.Dynamic.ExpandoObject();
            string categoriesJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/Categories");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            model.Categories = categories;
            if (alias != null)
            {
                string categoryJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/Categories/GetProductByAlias/" + alias);
                Category category = JsonConvert.DeserializeObject<Category>(categoryJson);
                model.Category = category;
            }
            return View(model);
        }

    }
}