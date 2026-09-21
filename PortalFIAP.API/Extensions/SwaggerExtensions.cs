using System.Reflection;
using Microsoft.OpenApi;

namespace PortalFiap.Extensions;

/// <summary>
/// Configuração do Swagger (Swashbuckle) do Portal FIAP.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Registra o gerador de documentação Swagger com metadados da API e comentários XML.
    /// </summary>
    public static IServiceCollection AddPortalSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Portal FIAP - Gestão Acadêmica",
                Version = "v1",
                Description = "API REST do portal acadêmico da FIAP. Gerencia alunos, cursos e turmas " +
                              "(com matrículas, bolsas e professores no domínio), seguindo Clean Architecture " +
                              "com .NET 10, EF Core e SQLite. Erros são retornados no formato ProblemDetails (RFC 7807)."
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// Habilita o Swagger e a Swagger UI (rota /swagger) somente em Development.
    /// </summary>
    public static WebApplication UsePortalSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal FIAP v1");
            options.RoutePrefix = "swagger";
        });

        return app;
    }
}
