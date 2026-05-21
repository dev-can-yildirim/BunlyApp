using BunlyWebApi.Context;
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
        public CategoriesController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Kategori ekleme işlemi gerçekleşti");
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
