﻿namespace SellIt.Core.Services.Count
{
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.ViewModels.Count;
    using SellIt.Infrastructure.Data;

    public class CountService : ICountService
    {
        private readonly ApplicationDbContext data;

        public CountService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public CountViewModel GetCount(int productId)
        {
            var count = new CountViewModel
            {
                ProductsToAprooveCount = this.data.Products.Where(s => s.IsAproved == false).Count(),
                AllProducts = this.data.Products.Count(),
                ProductMessages = this.data.Messages.Where(s => s.ProductId == productId).Count()
                + this.data.ReplyMessages.Where(s => s.Message.ProductId == productId).Count(),
            };

            return count;
        }
    }
}
