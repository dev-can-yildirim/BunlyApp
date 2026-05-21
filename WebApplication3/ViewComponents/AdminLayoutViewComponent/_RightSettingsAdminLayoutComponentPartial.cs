using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents.AdminLayoutViewComponent
{
    public class _RightSettingsAdminLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
