namespace SellIt.Api.Contracts.Auth
{
    using SellIt.Api.Models;

    public interface IAuthenticationManager
    {
        Task<bool> ValidateCredentials(AuthCredentials credentials);
        Task<string> CreateToken();
    }
}
