using BunlyWebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static BunlyWebUI.Controllers.AIController;

namespace BunlyWebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync("https://localhost:7165/api/Messages");
            if (responseMsg.IsSuccessStatusCode)
            {
                var jsonData = await responseMsg.Content.ReadAsStringAsync();
                var val = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(val);
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/Messages", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7165/api/Messages/{id}");
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMsg = await client.GetAsync($"https://localhost:7165/api/Messages/{id}");
            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
            return View(val);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateMessageDto);
            StringContent stringContent = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7165/api/Messages/", stringContent);
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> AnswerMessageWithAI(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMsg = await client.GetAsync($"https://localhost:7165/api/Messages/{id}");

            if (!responseMsg.IsSuccessStatusCode)
            {
                ViewBag.answerAI = "Mesaj bilgisi alınamadı.";
                return View();
            }

            var jsonData = await responseMsg.Content.ReadAsStringAsync();
            var val = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);

            if (val == null || string.IsNullOrWhiteSpace(val.MessageDetails))
            {
                ViewBag.answerAI = "Cevap oluşturmak için geçerli bir mesaj bulunamadı.";
                return View(val);
            }

            var prompt = val.MessageDetails;

            var apiKey = "AIzaSyCoE5X4uxt2P8BDc1LxL1_M1ottdPF362c";

            using var client2 = new HttpClient();

            var requestData = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = $@"
                        Sen bir restoran için kullanıcıların göndermiş olduğu mesajlara profesyonel cevaplar hazırlayan bir yapay zeka aracısın.
                        
                        Amacın:
                        - müşteri memnuniyetini korumak
                        - olumlu, nazik ve çözüm odaklı cevap vermek
                        - restoran adına profesyonel bir dil kullanmak
                        - gereksiz uzun olmayan ama yeterince açıklayıcı bir cevap oluşturmak
                        
                        Cevap kuralları:
                        - Türkçe cevap ver.
                        - Kullanıcıya doğrudan hitap et.
                        - Samimi ama kurumsal bir dil kullan.
                        - Sorun, şikayet veya öneri varsa yapıcı cevap ver.
                        - Restoran adına cevap yazıyormuş gibi davran.
                        - Cevabı yarıda kesme.
                        
                        Kullanıcının gönderdiği mesaj:
                        {prompt}
                        
                        Bu mesaja restoran adına verilecek en uygun cevabı yaz:"
                    }
                }
            }
        },
                generationConfig = new
                {
                    temperature = 0.5,
                    maxOutputTokens = 1200
                }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            var response = await client2.PostAsJsonAsync(url, requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GeminiResponse>();

                var content = result?
                    .Candidates?
                    .FirstOrDefault()?
                    .Content?
                    .Parts?
                    .FirstOrDefault()?
                    .Text;

                ViewBag.answerAI = content ?? "Cevap oluşturulamadı.";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.answerAI = "Bir hata oluştu: " + response.StatusCode + " - " + error;
            }

            return View(val);
        }

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMsg = await client.PostAsync("https://localhost:7165/api/Messages", stringContent);

            if (responseMsg.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }

            return View();
        }

    }
}
