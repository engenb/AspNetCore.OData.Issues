using System;
using System.IO;
using System.Reflection;
using Issue599.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Issue599.Infrastructure.Configuration
{
    public class SwaggerConfigurator :
              IConfigureOptions<SwaggerGenOptions>,
              IConfigureOptions<SwaggerOptions>,
              IConfigureOptions<SwaggerUIOptions>
    {
        private IApiVersionDescriptionProvider Provider { get; }

        public SwaggerConfigurator(IApiVersionDescriptionProvider provider)
        {
            Provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.DocumentFilter<VersionFilter>();
            options.OperationFilter<DefaultResponseFilter>();

            foreach (var description in Provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = "Issue 599",
                        Version = description.ApiVersion.ToString()
                    });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            }
        }

        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = "issue599/swagger/{documentName}/swagger.json";
        }

        public void Configure(SwaggerUIOptions options)
        {
            options.RoutePrefix = "issue599/explorer";
            options.ConfigObject.DisplayRequestDuration = true;

            foreach (var description in Provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/issue599/swagger/{description.GroupName}/swagger.json",
                    $"Issue 599 {description.GroupName}");
            }
        }
    }
}
