using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents.AdminLayoutViewComponent
{
    public class _NavbarAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
