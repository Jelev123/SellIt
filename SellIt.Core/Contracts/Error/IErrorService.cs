namespace SellIt.Core.Contracts.Error
{
    using SellIt.Core.ViewModels.Error;

    public interface IErrorService
    {
        ErrorResult ErrorMessage(string errorMessage);
    }
}
