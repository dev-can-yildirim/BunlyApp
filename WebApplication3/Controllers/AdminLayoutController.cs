using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
