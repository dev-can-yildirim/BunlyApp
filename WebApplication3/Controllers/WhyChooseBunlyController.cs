using BunlyWebUI.Dtos.WhyChooseBunlyDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BunlyWebUI.Controllers
{
    public class WhyChooseBunlyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhyChooseBunlyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> WhyChooseBunlyList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/Services");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultWhyChooseBunlyDto>>(jsonData);
                return View(val);
            }

            return View();
        }
        [HttpGet]
        public IActionResult CreateWhyChooseBunly()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWhyChooseBunly(CreateWhyChooseBunlyDto createWhyChooseBunlyDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createWhyChooseBunlyDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/services", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("WhyChooseBunlyList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteWhyChooseBunly(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7165/api/services/{id}");
            return RedirectToAction("WhyChooseBunlyList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWhyChooseBunly(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync($"https://localhost:7165/api/services/{id}");
            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetWhyChooseBunlyByIdDto>(jsonData);
            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWhyChooseBunly(UpdateWhyChooseBunlyDto updateWhyChooseBunlyDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateWhyChooseBunlyDto);
            StringContent stringContent = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7165/api/services/", stringContent);
            return RedirectToAction("WhyChooseBunlyList");
        }
    }
}
