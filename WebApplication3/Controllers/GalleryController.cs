using BunlyWebUI.Dtos.CategoryDtos;
using BunlyWebUI.Dtos.ImageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BunlyWebUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> ImageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/Images");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);
                return View(val);
            }

            return View();
        }
    }
}
