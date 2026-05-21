using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents
{
    public class _AboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
