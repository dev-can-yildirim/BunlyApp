using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.AboutDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public AboutsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            var value = _mapper.Map<About>(createAboutDto);

            _context.Abouts.Add(value);
            _context.SaveChanges();

            return Ok("Hakkımızda başarıyla eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {

            var cat = _context.Abouts.Find(id);
            if (cat == null)
            {
                return NotFound("Hakkımızda bulunamadı");
            }
            _context.Abouts.Remove(cat);
            _context.SaveChanges();

            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            return Ok(_context.Abouts.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetAboutById(int id)
        {
            return Ok(_context.Abouts.Find(id));
        }

        [HttpPut]
        public IActionResult UpdateAbout(About About)
        {
            _context.Abouts.Update(About);
            _context.SaveChanges();
            return Ok("Hakkımızda güncelleme başarılı.");
        }
    }
}
