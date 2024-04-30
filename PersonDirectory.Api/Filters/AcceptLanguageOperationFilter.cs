using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PersonDirectory.Api.Filters
{
    public class AcceptLanguageOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAcceptLanguageParameter = operation.Parameters.Any(parameter => parameter.Name == "Accept-Language");

            if (!hasAcceptLanguageParameter)
            {
                operation.Parameters ??= new List<OpenApiParameter>();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Accept-Language",
                    In = ParameterLocation.Header,
                    Required = false,
                    Description = "en-US, ka-GE"
                });
            }
        }
    }
}
