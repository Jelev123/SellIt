namespace SellIt.Infrastructure.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Image
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int ProductId { get; set; }

        public  Product Product { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system

        public string? RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public User AddedByUser { get; set; }
    }
}
