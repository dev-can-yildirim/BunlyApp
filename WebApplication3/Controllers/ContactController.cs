using BunlyWebUI.Dtos.ContactDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BunlyWebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ContactList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/Contacts");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                return View(val);
            }

            return View();
        }
        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/Contacts", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("ContactList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7165/api/Contacts?id=" + id);
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync($"https://localhost:7165/api/Contacts/{id}");
            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetContactByIdDto>(jsonData);
            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateContactDto);
            StringContent stringContent = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7165/api/Contacts/", stringContent);
            return RedirectToAction("ContactList");
        }
    }
}
