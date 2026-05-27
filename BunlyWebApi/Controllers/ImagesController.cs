using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.ImageDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public ImagesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateImage(CreateImageDto createImageDto)
        {
            var value = _mapper.Map<Image>(createImageDto);

            _context.Images.Add(value);
            _context.SaveChanges();

            return Ok("Görsel başarıyla eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteImage(int id)
        {

            var cat = _context.Images.Find(id);
            if (cat == null)
            {
                return NotFound("Görsel bulunamadı");
            }
            _context.Images.Remove(cat);
            _context.SaveChanges();

            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public IActionResult ImageList()
        {
            return Ok(_context.Images.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetImageById(int id)
        {
            return Ok(_context.Images.Find(id));
        }

        [HttpPut]
        public IActionResult UpdateImage(Image Image)
        {
            _context.Images.Update(Image);
            _context.SaveChanges();
            return Ok("Görsel güncelleme başarılı.");
        }
    }
}
