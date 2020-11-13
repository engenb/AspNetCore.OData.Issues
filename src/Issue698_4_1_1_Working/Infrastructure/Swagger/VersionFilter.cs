using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Issue698_4_1_1_Working.Infrastructure.Swagger
{
    public class VersionFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newPaths = new OpenApiPaths();
            foreach (var kvp in swaggerDoc.Paths)
            {
                newPaths[kvp.Key.Replace(
                    $"{{{nameof(swaggerDoc.Info.Version)}}}",
                    swaggerDoc.Info.Version,
                    StringComparison.InvariantCultureIgnoreCase)] = kvp.Value;
            }

            swaggerDoc.Paths = newPaths;
            var operations = typeof(OpenApiPathItem).GetProperties()
                .Where(x => x.PropertyType == typeof(OpenApiOperation))
                .ToList();

            foreach (var path in swaggerDoc.Paths.Values)
            {
                foreach (var prop in operations)
                {
                    if (prop.GetValue(path) is OpenApiOperation op &&
                        op.Parameters.FirstOrDefault(x => x.Name.Equals(
                            nameof(swaggerDoc.Info.Version),
                            StringComparison.InvariantCultureIgnoreCase)) is OpenApiParameter versionParam)
                    {
                        op.Parameters.Remove(versionParam);
                    }
                }
            }
        }
    }
}
