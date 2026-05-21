using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
