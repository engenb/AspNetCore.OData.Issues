using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Issue702.Infrastructure.Configuration
{
    public class ApiConfigurator :
        IPostConfigureOptions<MvcOptions>,
        IConfigureOptions<ApiVersioningOptions>,
        IConfigureOptions<ODataApiExplorerOptions>,
        IConfigureOptions<RouteOptions>,
        IConfigureOptions<ForwardedHeadersOptions>,
        IConfigureOptions<JsonOptions>
    {
        public void PostConfigure(string name, MvcOptions options)
        {
            options.EnableEndpointRouting = false;

            foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
            {
                outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
            }
            foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
            {
                inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
            }
        }

        public void Configure(ApiVersioningOptions options)
        {
            options.ReportApiVersions = true;
        }

        public void Configure(ODataApiExplorerOptions options)
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        }

        public void Configure(RouteOptions options)
        {
            options.LowercaseUrls = true;
        }

        public void Configure(ForwardedHeadersOptions options)
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
        }

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.IgnoreNullValues = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }
    }
}
