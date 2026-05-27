using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BunlyWebUI.Controllers
{
    public class AIController : Controller
    {
        public IActionResult CreateRecipeWithOpenAI()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
        {
            var apiKey = "YENI_GEMINI_API_KEYIN";

            using var client = new HttpClient();

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
Sen bir restoran için yemek önerileri üreten bir yapay zeka aracısın.

Amacın, kullanıcı tarafından girilen malzemelere göre profesyonel ve uygulanabilir bir yemek tarifi önermektir.

Cevabı şu formatta ver:

Yemek Adı:
Kısa Açıklama:
Malzemeler:
Hazırlanışı:
Pişirme Süresi:
Servis Önerisi:

Kurallar:
- Cevabı Türkçe ver.
- Tarif yarıda kesilmesin.
- Kullanıcının verdiği malzemelere uygun öneri yap.
- Gereksiz uzun yazma ama açıklayıcı ol.

Kullanıcının girdiği malzemeler:
{prompt}"
                    }
                }
            }
        },
                generationConfig = new
                {
                    temperature = 0.5,
                    maxOutputTokens = 2500
                }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var response = await client.PostAsJsonAsync(url, requestData);

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

                ViewBag.recipe = content ?? "Tarif oluşturulamadı.";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.recipe = "Bir hata oluştu: " + response.StatusCode + " - " + errorMessage;
            }

            return View();
        }

        public class GeminiResponse
        {
            public List<GeminiCandidate> Candidates { get; set; }
        }

        public class GeminiCandidate
        {
            public GeminiContent Content { get; set; }
        }

        public class GeminiContent
        {
            public List<GeminiPart> Parts { get; set; }
        }

        public class GeminiPart
        {
            public string Text { get; set; }
        }

    }
}
