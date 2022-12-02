namespace SellIt.Infrastructure.Data.Models
{
    public class ReplyMessage
    {
        public int Id { get; set; }

        public string ReplyerUserId { get; set; }

        public string ReplayerUserName { get; set; }

        public int MessageId { get; set; }

        public string ReplyText { get; set; }

        public DateTime Date { get; set; }

        public Message Message { get; set; }

        public User ReplyerUser { get; set; }
    }
}
