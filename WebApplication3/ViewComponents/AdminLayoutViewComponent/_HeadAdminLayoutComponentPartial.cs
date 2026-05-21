using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents.AdminLayoutViewComponent
{
    public class _HeadAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
