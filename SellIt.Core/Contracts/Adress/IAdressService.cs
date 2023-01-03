namespace SellIt.Core.Contracts.Adress
{
    public interface IAdressService
    {
        Task<string> GetIPAddress();

        Task GetGeoInfo();
    }
}
