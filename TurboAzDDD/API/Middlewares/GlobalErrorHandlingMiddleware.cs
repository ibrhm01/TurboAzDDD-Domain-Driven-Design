using System.Net;
using System.Text.Json;
using API.Middlewares;
using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                var errorResponse = new ErrorResponse { Message = e.Message };
                var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(jsonErrorResponse);

            }
            catch (DuplicateNameException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var errorResponse = new ErrorResponse { Message = e.Message };
                var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(jsonErrorResponse);
            }
        }
    }
}

