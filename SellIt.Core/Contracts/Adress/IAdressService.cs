namespace SellIt.Core.Contracts.Adress
{
    using SellIt.Core.ViewModels.Adress;

    public interface IAdressService
    {
        AddressByUserId AddressByUserId(string userId);
    }
}
