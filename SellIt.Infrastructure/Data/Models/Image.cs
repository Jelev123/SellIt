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

        public string Name { get; set; }

        public string? URL { get; set; }

        public string AddedByUserId { get; set; }

        public User AddedByUser { get; set; }
    }
}
