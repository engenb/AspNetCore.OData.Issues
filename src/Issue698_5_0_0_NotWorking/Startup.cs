using Issue698_5_0_0_NotWorking.Infrastructure.Configuration;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Issue698_5_0_0_NotWorking
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureOptions<ApiConfigurator>()
                .ConfigureOptions<SwaggerConfigurator>();

            services
                .AddHttpContextAccessor()
                .AddResponseCompression()
                .AddRouting()
                .AddApiVersioning()
                .AddODataApiExplorer()
                .AddSwaggerGen();

            services
                .AddOData()
                .EnableApiVersioning();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VersionedODataModelBuilder modelBuilder)
        {
            app
                .UseResponseCompression()
                .UseForwardedHeaders()
                .UseWhen(ctx => ctx.Request.Path.Value.StartsWith("/issue698_500_notworking/v"), api =>
                {
                    api
                        .UseCors(policy => policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                    //.UseAuthentication()
                })
                .UseMvc(route => route
                    .SetUrlKeyDelimiter(ODataUrlKeyDelimiter.Parentheses)
                    .MapVersionedODataRoute(
                        "odata",
                        "issue698_500_notworking/v{version:apiVersion}",
                        modelBuilder.GetEdmModels()))
                .UseSwagger()
                .UseSwaggerUI();
        }
    }
}
