namespace SellIt.Infrastructure.Data.Models
{
    public class AllMessages
    {
        public int Id { get; set; }

        public string UserSenderId { get; set; }

        public string UserReplyerId { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
