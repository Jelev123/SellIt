namespace SellIt.Api.Services.Auth
{
    using SellIt.Api.Models;

    public interface IAuthenticationManager
    {
        Task<bool> ValidateCredentials(AuthCredentials credentials);
        Task<string> CreateToken();
    }
}
