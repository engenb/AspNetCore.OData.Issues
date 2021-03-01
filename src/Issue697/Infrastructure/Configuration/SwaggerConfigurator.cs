﻿using System;
using System.IO;
using System.Reflection;
using Issue697.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Issue697.Infrastructure.Configuration
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
                        Title = "Issue 697",
                        Version = description.ApiVersion.ToString()
                    });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            }
        }

        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = "issue697/swagger/{documentName}/swagger.json";
        }

        public void Configure(SwaggerUIOptions options)
        {
            options.RoutePrefix = "issue697/explorer";
            options.ConfigObject.DisplayRequestDuration = true;

            foreach (var description in Provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/issue697/swagger/{description.GroupName}/swagger.json",
                    $"Issue 697 {description.GroupName}");
            }
        }
    }
}
