using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Issue698_4_1_1_Working.Infrastructure.Swagger
{
    public class DefaultResponseFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
                context.MethodInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null ||
                context.MethodInfo.DeclaringType.GetCustomAttribute<AuthorizeAttribute>(true) != null;

            if (hasAuthorize)
            {
                AddResponse(operation, HttpStatusCode.Unauthorized);
                AddResponse(operation, HttpStatusCode.Forbidden);
            }

            AddResponse(operation, HttpStatusCode.InternalServerError);
            AddResponse(operation, HttpStatusCode.NotFound);
        }

        private static void AddResponse(OpenApiOperation op, HttpStatusCode code)
        {
            if (!op.Responses.ContainsKey($"{(int)code}"))
            {
                op.Responses[$"{(int)code}"] = new OpenApiResponse { Description = $"{code}" };
            }
        }
    }
}
