using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents.DefaultMenuViewComponent
{
    public class _DefaultMenuViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
