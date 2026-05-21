using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.NotificationDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tüm Notification listesini getir
        [HttpGet]
        public IActionResult GetNotificationList()
        {
            var values = _context.Notifications.ToList();
            var result = _mapper.Map<List<ResultNotificationDto>>(values);

            return Ok(result);
        }

        // Id'ye göre Notification getir
        [HttpGet("{id}")]
        public IActionResult GetNotificationById(int id)
        {
            var value = _context.Notifications.Find(id);

            if (value == null)
            {
                return NotFound("Notification bulunamadı.");
            }

            var result = _mapper.Map<GetNotificationByIdDto>(value);

            return Ok(result);
        }

        // Yeni Notification ekle
        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var value = _mapper.Map<Notification>(createNotificationDto);

            _context.Notifications.Add(value);
            _context.SaveChanges();

            return Ok("Notification başarıyla eklendi.");
        }

        // Notification güncelle
        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var value = _context.Notifications.Find(updateNotificationDto.NotificationId);

            if (value == null)
            {
                return NotFound("Güncellenecek Notification bulunamadı.");
            }

            _mapper.Map(updateNotificationDto, value);

            _context.SaveChanges();

            return Ok("Notification başarıyla güncellendi.");
        }

        // Notification sil
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var value = _context.Notifications.Find(id);

            if (value == null)
            {
                return NotFound("Silinecek Notification bulunamadı.");
            }

            _context.Notifications.Remove(value);
            _context.SaveChanges();

            return Ok("Notification başarıyla silindi.");
        }
    }
}
