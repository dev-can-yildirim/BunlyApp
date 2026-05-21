using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents.AdminLayoutViewComponent
{
    public class _SidebarAdminLayoutComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
