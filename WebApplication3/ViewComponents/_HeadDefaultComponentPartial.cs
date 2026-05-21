using BunlyWebApi.Context;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebUI.ViewComponents
{
    public class _HeadDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
