using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Advance.NET7.MinimalApi.Utility
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            const string FileUploadContentType = "multipart/form-data";
            if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x =>
            x.Key.Equals(FileUploadContentType,StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }

            if (context.ApiDescription.ParameterDescriptions[0].Type == typeof(HttpRequest))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = "文件上传",
                    Content = new Dictionary<String, OpenApiMediaType>
                    {
                        {
                            FileUploadContentType, new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Required = new HashSet<String>{ "file" },
                                    Properties = new Dictionary<String,OpenApiSchema>
                                    {
                                        {
                                            "file", new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Format = "binary"
                                            }
                                        }
                                    }
                                }   
                            }
                        }
                    }
                };
            }
        }
    }
}

