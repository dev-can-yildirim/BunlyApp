using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.FeatureDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tüm Feature listesini getir
        [HttpGet]
        public IActionResult GetFeatureList()
        {
            var values = _context.Features.ToList();
            var result = _mapper.Map<List<ResultFeatureDto>>(values);

            return Ok(result);
        }

        // Id'ye göre Feature getir
        [HttpGet("{id}")]
        public IActionResult GetFeatureById(int id)
        {
            var value = _context.Features.Find(id);

            if (value == null)
            {
                return NotFound("Feature bulunamadı.");
            }

            var result = _mapper.Map<GetByIdFeatureDto>(value);

            return Ok(result);
        }

        // Yeni Feature ekle
        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var value = _mapper.Map<Feature>(createFeatureDto);

            _context.Features.Add(value);
            _context.SaveChanges();

            return Ok("Feature başarıyla eklendi.");
        }

        // Feature güncelle
        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var value = _context.Features.Find(updateFeatureDto.FeatureId);

            if (value == null)
            {
                return NotFound("Güncellenecek Feature bulunamadı.");
            }

            _mapper.Map(updateFeatureDto, value);

            _context.SaveChanges();

            return Ok("Feature başarıyla güncellendi.");
        }

        // Feature sil
        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var value = _context.Features.Find(id);

            if (value == null)
            {
                return NotFound("Silinecek Feature bulunamadı.");
            }

            _context.Features.Remove(value);
            _context.SaveChanges();

            return Ok("Feature başarıyla silindi.");
        }
    }
}
