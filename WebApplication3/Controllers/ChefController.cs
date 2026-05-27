using BunlyWebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BunlyWebUI.Controllers
{
    public class ChefController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ChefList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/Chefs");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
                return View(val);
            }

            return View();
        }
        [HttpGet]
        public IActionResult CreateChef()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createChefDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/Chefs", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("ChefList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7165/api/Chefs/{id}" );
            return RedirectToAction("ChefList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync($"https://localhost:7165/api/Chefs/{id}");
            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetChefByIdDto>(jsonData);
            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateChefDto);
            StringContent stringContent = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7165/api/Chefs/", stringContent);
            return RedirectToAction("ChefList");
        }
    }
}
