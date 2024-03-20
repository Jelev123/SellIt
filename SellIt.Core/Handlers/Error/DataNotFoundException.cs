namespace SellIt.Core.Handlers.Error
{
    using System;

    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        {
            
        }
    }
}
