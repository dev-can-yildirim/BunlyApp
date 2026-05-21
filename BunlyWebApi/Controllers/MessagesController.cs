using AutoMapper;
using BunlyWebApi.Context;
using BunlyWebApi.Dtos.MessageDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public MessagesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tüm mesajları getir
        [HttpGet]
        public IActionResult GetMessageList()
        {
            var values = _context.Messages.ToList();
            var result = _mapper.Map<List<ResultMessageDto>>(values);

            return Ok(result);
        }

        // Id'ye göre mesaj getir
        [HttpGet("{id}")]
        public IActionResult GetMessageById(int id)
        {
            var value = _context.Messages.Find(id);

            if (value == null)
            {
                return NotFound("Mesaj bulunamadı.");
            }

            var result = _mapper.Map<GetByIdMessageDto>(value);

            return Ok(result);
        }

        // Yeni mesaj ekle
        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            var value = _mapper.Map<Message>(createMessageDto);

            _context.Messages.Add(value);
            _context.SaveChanges();

            return Ok("Mesaj başarıyla eklendi.");
        }

        // Mesaj güncelle
        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var value = _context.Messages.Find(updateMessageDto.MessageId);

            if (value == null)
            {
                return NotFound("Mesaj bulunamadı.");
            }

            _mapper.Map(updateMessageDto, value);

            _context.SaveChanges();

            return Ok("Mesaj başarıyla güncellendi.");
        }

        // Mesaj sil
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);

            if (value == null)
            {
                return NotFound("Silinecek mesaj bulunamadı.");
            }

            _context.Messages.Remove(value);
            _context.SaveChanges();

            return Ok("Mesaj başarıyla silindi.");
        }

        [HttpGet("MessagesListByIsReadyFalse")]
        public IActionResult MessagesListByIsReadyFalse()
        {
            var val = _context.Messages.Where(x => x.IsRead == false).ToList();
            return Ok(val);
        }
    }
}
