using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace CallInDoor.Config.Extentions
{
    public static class SwaggerExtention
    {
        public static IServiceCollection AddOurSwaager(this IServiceCollection services)
        {



            // Swagger service properties
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "callInDoor API",
                    Description = "callIndoors api",
                    TermsOfService = new Uri(PublicHelper.swaggerProductionUrl),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri(PublicHelper.swaggerProductionUrl),
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = "sina Fallahpour",
                    //    Url = new Uri("https://example.com/license"),
                    //}
                });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                      {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                        });
            });

            return services;
        }




        // Swagger service properties
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new Info
        //        {
        //            Version = "v1",
        //            Title = "Auth JWT",
        //            Description = "Auth JWT Demo - calln door",
        //            TermsOfService = "None",
        //            Contact = new Contact()
        //            {
        //                Name = "Sina Falahour",
        //                Email = "Sina@Fallahpour",
        //                Url = "http://wwwcallindoor.com"
        //            }
        //        });
        //        c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
        //        {
        //            Description = "JWT Authorization header {token}",
        //            Name = "Authorization",
        //            In = "header",
        //            Type = "apiKey"
        //        });
        //        c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
        //        {
        //            { "Bearer", new string[] { } }
        //        });
        //    });
        //    return services;
        //}
    }
}
