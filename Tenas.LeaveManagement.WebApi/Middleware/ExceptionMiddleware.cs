using Newtonsoft.Json;
using System.Net;
using Tenas.LeaveManagement.Application.Exceptions;

namespace Tenas.LeaveManagement.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var result = new ErrorDetails 
            {
                StatusCode = (int) statusCode,
                Message = ex.Message,
            };

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = new ErrorDetails
                    {
                        StatusCode = (int)statusCode,
                        Message = badRequestException.Message,
                        Route = context.Request.Path
                    };
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = new ErrorDetails
                    {
                        StatusCode = (int) statusCode,
                        Message = validationException.Errors,
                        Route = context.Request.Path
                    };
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    result = new ErrorDetails
                    {
                        StatusCode = (int)statusCode,
                        Message = notFoundException.Message,
                        Route = context.Request.Path
                    };
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int) statusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; } = 500;
        public object Message { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
    }
}
