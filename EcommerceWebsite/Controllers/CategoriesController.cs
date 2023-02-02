using EcommerceWebsite.Helpper;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceWebsite.Controllers
{
    [Route("danh-muc")]
    public class CategoriesController : Controller
    {

        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }
        
       // [Route("{alias?}")]
        public async Task<IActionResult> Index(string? alias)
        {
            dynamic model = new System.Dynamic.ExpandoObject();
            string categoriesJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/Categories");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            model.Categories = categories;
            model.Alias = alias;
            if (alias != null)
            {
                string categoryJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/Categories/GetProductByAlias/" + alias);
                List<Category> categoryWithProduct = JsonConvert.DeserializeObject<List<Category>>(categoryJson);
                model.CategoryWithProduct = categoryWithProduct;
            }
            else
            {
                string categoryJson = await RestAPI.GetJSON("https://" + Request.Host.Value + "/api/Categories/GetProductByAlias/" + "all");
                List<Category> categoryWithProduct = JsonConvert.DeserializeObject<List<Category>>(categoryJson);
                model.CategoryWithProduct = categoryWithProduct;
            }
            return View(model);
        }


    }
}