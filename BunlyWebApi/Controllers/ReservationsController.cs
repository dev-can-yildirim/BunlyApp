using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.RezervationDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public ReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var value = _mapper.Map<Reservation>(createReservationDto);

            _context.Reservations.Add(value);
            _context.SaveChanges();

            return Ok("Rezervazyon başarıyla eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteReservation(int id)
        {

            var cat = _context.Reservations.Find(id);
            if (cat == null)
            {
                return NotFound("Rezervazyon bulunamadı");
            }
            _context.Reservations.Remove(cat);
            _context.SaveChanges();

            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public IActionResult ReservationList()
        {
            return Ok(_context.Reservations.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            return Ok(_context.Reservations.Find(id));
        }

        [HttpPut]
        public IActionResult UpdateReservation(Reservation Reservation)
        {
            _context.Reservations.Update(Reservation);
            _context.SaveChanges();
            return Ok("Rezervazyon güncelleme başarılı.");
        }
    }
}
