namespace BunlyWebApi.Dtos.MessageDtos
{
    public class UpdateMessageDto
    {
        public int MessageId { get; set; }
        public string NameSurname { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string MessageDetails { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
    }
}
