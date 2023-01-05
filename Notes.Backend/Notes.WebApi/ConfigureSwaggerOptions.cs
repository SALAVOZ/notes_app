using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Notes.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider=provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName, 
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Notes API {apiVersion}",
                        Description = "A ASP NET CORE Web API",
                        TermsOfService = new Uri("https://github.com/SALAVOZ/notes_app")
                    });
                options.AddSecurityDefinition($"AuthToken {apiVersion}",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Description = "Authorization token"
                    }
                    );

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = $"Auth Token {apiVersion}"
                            },
                        },
                     new string[] {}
                     }
                });

                options.CustomOperationIds(apiDescriptions => 
                    apiDescriptions.TryGetMethodInfo(out MethodInfo methodInfo)
                    ? methodInfo.Name : null);
            }
        }
    }
}
