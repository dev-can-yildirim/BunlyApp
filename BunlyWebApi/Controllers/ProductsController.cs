using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.ProductDtos;
using BunlyWebApi.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<Product> _validator;

        public ProductsController(ApiContext context, IMapper mapper, IValidator<Product> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _context.Products.ToList();
            var result = _mapper.Map<List<ResultProductDto>>(values);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _context.Products.Find(id);

            if (value == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            var result = _mapper.Map<ResultProductDto>(value);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            var validationResult = _validator.Validate(product);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok("Ürün başarıyla eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var value = _context.Products.Find(updateProductDto.ProductId);

            if (value == null)
            {
                return NotFound("Güncellenecek ürün bulunamadı.");
            }

            _mapper.Map(updateProductDto, value);

            var validationResult = _validator.Validate(value);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            _context.Products.Update(value);
            _context.SaveChanges();

            return Ok("Ürün başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);

            if (value == null)
            {
                return NotFound("Silinecek ürün bulunamadı.");
            }

            _context.Products.Remove(value);
            _context.SaveChanges();

            return Ok("Ürün başarıyla silindi.");
        }
        //17.videodaki createproductwithcategory eklenmedi


        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var value = _context.Products.Include(x => x.Category).ToList();
            var products = _mapper.Map<List<ResultProductWithCategoryDto>>(value);

            return Ok(products);
        }
    }
}