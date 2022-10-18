﻿namespace SellIt.Infrastructure.Data.Models
{
    public class LikedProduct
    {
        public int Id { get; set; }

        public int LikedCount { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
