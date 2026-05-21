using BunlyWebApi.Context;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ServicesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var values = await _context.Services.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var value = await _context.Services.FindAsync(id);

            if (value == null)
            {
                return NotFound("Servis bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return Ok("Servis başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(Service service)
        {
            var value = await _context.Services.FindAsync(service.ServiceId);

            if (value == null)
            {
                return NotFound("Güncellenecek servis bulunamadı.");
            }

            value.Title = service.Title;
            value.Description = service.Description;
            value.IconUrl = service.IconUrl;

            await _context.SaveChangesAsync();

            return Ok("Servis başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var value = await _context.Services.FindAsync(id);

            if (value == null)
            {
                return NotFound("Silinecek servis bulunamadı.");
            }

            _context.Services.Remove(value);
            await _context.SaveChangesAsync();

            return Ok("Servis başarıyla silindi.");
        }
    }
}