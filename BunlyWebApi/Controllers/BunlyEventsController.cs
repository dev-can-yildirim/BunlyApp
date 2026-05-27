using BunlyWebApi.Context;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BunlyEventsController : ControllerBase
    {
        private readonly ApiContext _context;
        public BunlyEventsController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateBunlyEvent(BunlyEvent BunlyEvent)
        {
            _context.BunlyEvents.Add(BunlyEvent);
            _context.SaveChanges();
            return Ok("Kategori ekleme işlemi gerçekleşti");
        }

            [HttpDelete]
            public IActionResult DeleteBunlyEvent(int id)
            {

                var cat = _context.BunlyEvents.Find(id);
                if (cat == null)
                {
                    return NotFound("Kategori bulunamadı");
                }
                _context.BunlyEvents.Remove(cat);
                _context.SaveChanges();

                return Ok("Silme işlemi başarılı");
            }

        [HttpGet]
        public IActionResult BunlyEventList()
        {
            return Ok(_context.BunlyEvents.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetBunlyEventById(int id)
        {
            return Ok(_context.BunlyEvents.Find(id));
        }

        [HttpPut]
        public IActionResult UpdateBunlyEvent(BunlyEvent BunlyEvent)
        {
            _context.BunlyEvents.Update(BunlyEvent);
            _context.SaveChanges();
            return Ok("Kategori güncelleme başarılı.");
        }
    }
}
