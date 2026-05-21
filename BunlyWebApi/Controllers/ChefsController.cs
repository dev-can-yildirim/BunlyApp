using BunlyWebApi.Context;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChefsController(ApiContext context)
        {
            _context = context;
        }

        // Tüm şefleri getir
        [HttpGet]
        public IActionResult GetChefs()
        {
            var values = _context.Chefs.ToList();
            return Ok(values);
        }

        // Id'ye göre şef getir
        [HttpGet("{id}")]
        public IActionResult GetChefById(int id)
        {
            var value = _context.Chefs.Find(id);

            if (value == null)
            {
                return NotFound("Şef bulunamadı.");
            }

            return Ok(value);
        }

        // Yeni şef ekle
        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            _context.Chefs.Add(chef);
            _context.SaveChanges();

            return Ok("Şef başarıyla eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            var value = _context.Chefs.Find(chef.ChefId);

            if (value == null)
            {
                return NotFound("Güncellenecek şef bulunamadı.");
            }

            value.NameSurname = chef.NameSurname;
            value.Title = chef.Title;
            value.Description = chef.Description;
            value.ImageUrl = chef.ImageUrl;

            _context.SaveChanges();

            return Ok("Şef başarıyla güncellendi.");
        }

        // Şef sil
        [HttpDelete("{id}")]
        public IActionResult DeleteChef(int id)
        {
            var value = _context.Chefs.Find(id);

            if (value == null)
            {
                return NotFound("Silinecek şef bulunamadı.");
            }

            _context.Chefs.Remove(value);
            _context.SaveChanges();

            return Ok("Şef başarıyla silindi.");
        }
    }
}
