using Domain.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallInDoor.Config.Middleware
{
    public class ErrorWrappingMiddleware2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorWrappingMiddleware2> _logger;

        public ErrorWrappingMiddleware2(RequestDelegate next, ILogger<ErrorWrappingMiddleware2> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch  
            {
                //_logger.LogError(EventIds.GlobalException, ex, ex.Message);
                context.Response.StatusCode = 500;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = new ApiResponse(context.Response.StatusCode);

                var json = JsonConvert.SerializeObject(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
