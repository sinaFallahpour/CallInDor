using Domain.DTO.Response;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            catch(Exception ex)
            {
                //_logger.LogError(EventIds.GlobalException, ex, ex.Message);
                context.Response.StatusCode = 500;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                List<string> error = new List<string> { PubicMessages.InternalServerMessage };
                var response = new ApiBadRequestResponse(error, 500);
                //var response = new ApiResponse(context.Response.StatusCode);

                var defaultConst = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
                {
                    ContractResolver = defaultConst,
                    Formatting = Formatting.Indented
                });


                //var json = JsonConvert.SerializeObject(response,);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
