using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Easynvest.Investment.Portfolio.API.Swagger.Filters
{
    public class TagsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new OpenApiTag[]
            {
                new OpenApiTag{Name = "Aplicação", Description = "Serviços gerais da aplicação"},
                new OpenApiTag{Name = "Investimentos", Description = "Serviço para consulta de Investimentos"}
            };
        }
    }
}