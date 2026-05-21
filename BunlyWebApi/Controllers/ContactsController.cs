using BunlyWebApi.Context;
using BunlyWebApi.Dtos.ContactDtos;
using BunlyWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ContactsController(ApiContext context)
        {
            _context = context;
        }

        // Tüm iletişim bilgilerini getir
        [HttpGet]
        public IActionResult GetContacts()
        {
            var values = _context.Contacts.ToList();
            return Ok(values);
        }

        // Id'ye göre iletişim bilgisi getir
        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var value = _context.Contacts.Find(id);

            if (value == null)
            {
                return NotFound("İletişim bilgisi bulunamadı.");
            }

            return Ok(value);
        }

        // Yeni iletişim bilgisi ekle
        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            Contact contact = new Contact();
            contact.Email = createContactDto.Email;
            contact.Address = createContactDto.Address;
            contact.PhoneNumber = createContactDto.PhoneNumber;
            contact.MapLocation = createContactDto.MapLocation;
            contact.OpenHours = createContactDto.OpenHours;


            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return Ok("İletişim bilgisi başarıyla eklendi.");
        }

        // İletişim bilgisi güncelle
        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            Contact contact = new Contact();
            contact.ContactId = updateContactDto.ContactId;
            contact.Email = updateContactDto.Email;
            contact.Address = updateContactDto.Address;
            contact.PhoneNumber = updateContactDto.PhoneNumber;
            contact.OpenHours = updateContactDto.OpenHours;
            contact.MapLocation = updateContactDto.MapLocation;
            
            _context.Contacts.Update(contact);
            _context.SaveChanges();

            return Ok("İletişim bilgisi başarıyla güncellendi.");
        }

        // İletişim bilgisi sil
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var value = _context.Contacts.Find(id);

            if (value == null)
            {
                return NotFound("Silinecek iletişim bilgisi bulunamadı.");
            }

            _context.Contacts.Remove(value);
            _context.SaveChanges();

            return Ok("İletişim bilgisi başarıyla silindi.");
        }
    }
}
