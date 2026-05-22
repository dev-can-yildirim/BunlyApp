using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.CategoryDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public CategoriesController(ApiContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);

            _context.Categories.Add(value);
            _context.SaveChanges();

            return Ok("Kategori başarıyla eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {

            var cat = _context.Categories.Find(id);
            if (cat == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            _context.Categories.Remove(cat);
            _context.SaveChanges();

            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            return Ok(_context.Categories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_context.Categories.Find(id));
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Ok("Kategori güncelleme başarılı.");
        }
    }
}
