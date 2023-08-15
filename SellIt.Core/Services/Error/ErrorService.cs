namespace SellIt.Core.Services.Error
{
    using SellIt.Core.Contracts.Error;
    using SellIt.Core.ViewModels.Error;

    public class ErrorService : IErrorService
    {
        public ErrorResult ErrorMessage(string errorMessage)
        {
            return new ErrorResult
            {
                Message = errorMessage,
            };
        }
    }
}
