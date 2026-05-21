using BunlyWebApi.Context;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TestimonialsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestimonial()
        {
            var values = await _context.Testimonials.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestimonial(int id)
        {
            var value = await _context.Testimonials.FindAsync(id);

            if (value == null)
            {
                return NotFound("Servis bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(Testimonial Testimonial)
        {
            await _context.Testimonials.AddAsync(Testimonial);
            await _context.SaveChangesAsync();

            return Ok("Servis başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestimonial(Testimonial Testimonial)
        {
            var value = await _context.Testimonials.FindAsync(Testimonial.TestimonialId);

            if (value == null)
            {
                return NotFound("Güncellenecek servis bulunamadı.");
            }

            value.Title = Testimonial.Title;
            value.TestimonialId = Testimonial.TestimonialId;
            value.Comment = Testimonial.Comment;
            value.ImageUrl = Testimonial.ImageUrl;
            value.NameSurname = Testimonial.NameSurname;



            await _context.SaveChangesAsync();

            return Ok("Servis başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var value = await _context.Testimonials.FindAsync(id);

            if (value == null)
            {
                return NotFound("Silinecek servis bulunamadı.");
            }

            _context.Testimonials.Remove(value);
            await _context.SaveChangesAsync();

            return Ok("Servis başarıyla silindi.");
        }
    }
}
