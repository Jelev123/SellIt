namespace SellIt.Core.Services.Adress
{
    using SellIt.Core.Contracts.Adress;
    using SellIt.Core.ViewModels.Adress;
    using SellIt.Infrastructure.Data;

    public class AdressService : IAdressService
    {
        private readonly ApplicationDbContext data;

        public AdressService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public  AddressByUserId AddressByUserId(string userId)
        {

            var user = this.data.Users.
                Where(s => s.Id == userId).
                Select(s => new AddressByUserId
                {
                    City = s.Adress.City,
                    Id = s.AdressId,
                }).
                FirstOrDefault();

            return user;
              
        }
    }
}
