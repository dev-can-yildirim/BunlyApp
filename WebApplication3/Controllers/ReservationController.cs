using BunlyWebUI.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BunlyWebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ReservationList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/Reservations");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
                return View(val);
            }

            return View();
        }
        [HttpGet]
        public IActionResult CreateReservation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createReservationDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/Reservations", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("ReservationList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7165/api/Reservations?id=" + id);
            return RedirectToAction("ReservationList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync($"https://localhost:7165/api/Reservations/{id}");
            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetReservationByIdDto>(jsonData);
            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateReservationDto);
            StringContent stringContent = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7165/api/Reservations/", stringContent);
            return RedirectToAction("ReservationList");
        }
    }
}
