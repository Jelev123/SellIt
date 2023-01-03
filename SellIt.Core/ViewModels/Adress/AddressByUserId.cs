namespace SellIt.Core.ViewModels.Adress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AddressByUserId
    {
        public string Id { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string RegionCode { get; set; }

        public string RegionName { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public decimal Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
