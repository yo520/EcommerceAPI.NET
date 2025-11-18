using Ecommerce.Domain.Exceptions;
using Ecommerce.Shared.ErrorModels;

namespace Ecommerce.Web.CustomMiddleWares
{
    public class CustomExeptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public CustomExeptionMiddleWare(RequestDelegate Next,ILogger<CustomExeptionMiddleWare> logger)
        {
            next = Next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var errorResponse = new ErrorToReturn()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = $"End Point {context.Request.Path} Not Found",
                    };
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                var errorResponse = new ErrorToReturn()
                {
                    Message = ex.Message,
                };

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException=>StatusCodes.Status401Unauthorized,
                    BadRequesException badRequesException=>GetBadRequestStatusCode(badRequesException, errorResponse),
                    _ => StatusCodes.Status500InternalServerError

                };
                context.Response.ContentType = "application/json";

                errorResponse.StatusCode = context.Response.StatusCode;
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }

        private int GetBadRequestStatusCode(BadRequesException ex,ErrorToReturn Response)
        {
            Response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
