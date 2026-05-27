using BunlyWebUI.Dtos.EventDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BunlyWebUI.Controllers
{
    public class BunlyEventController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BunlyEventController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> BunlyEventList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/BunlyEvents");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultEventDto>>(jsonData);
                return View(val);
            }

            return View();
        }
        [HttpGet]
        public IActionResult CreateBunlyEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBunlyEvent(CreateEventDto createBunlyEventDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBunlyEventDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/BunlyEvents", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("BunlyEventList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteBunlyEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7165/api/BunlyEvents?id=" + id);
            return RedirectToAction("BunlyEventList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBunlyEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync($"https://localhost:7165/api/BunlyEvents/{id}");
            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetEventByIdDto>(jsonData);
            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBunlyEvent(UpdateEventDto updateBunlyEventDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBunlyEventDto);
            StringContent stringContent = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7165/api/BunlyEvents/", stringContent);
            return RedirectToAction("BunlyEventList");
        }
    }
}
