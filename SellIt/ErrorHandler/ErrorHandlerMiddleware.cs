namespace SellIt.Core.Handlers.Error
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.Handlers.Error.Class;
    using System.Net;
    using System.Text.Json;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ArgumentException ae:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case DataNotFoundException dnfe:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NullReferenceException nre:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new ErrorMessage { Message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
