using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFIAP.Application.Services;
using PortalFiap.Extensions;
using PortalFiap.Infrastructure.Persistence;
using PortalFiap.Infrastructure.Persistence.Repositories;
using PortalFiap.Seed;

namespace PortalFiap;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Enums são serializados pelo nome (ex.: "AnaliseEDesenvolvimentoDeSistemas") em vez de número.
        // RespectRequiredConstructorParameters: campo ausente no JSON dos DTOs (records) vira 400,
        // em vez de assumir o valor padrão (ex.: curso sem "nome" virava ADS).
        builder.Services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                o.JsonSerializerOptions.RespectRequiredConstructorParameters = true;
            });

        builder.Services.AddPortalExceptionHandling();
        builder.Services.AddPortalHealthChecks();

        builder.Services.AddDbContext<PortalFiapContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("PortalFiapSQLiteConnection");
                options.UseSqlite(connectionString);
            }
        );

        // Dependency Injection Configuration
        builder.Services.AddScoped<IAlunoService, AlunoService>();
        builder.Services.AddScoped<ICursoService, CursoService>();
        builder.Services.AddScoped<ITurmaService, TurmaService>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<ICursoRepository, CursoRepository>();
        builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddPortalSwagger();

        var app = builder.Build();

        try
        {
            await DatabaseSeeder.SeedAsync(app.Services);
        }
        catch (Exception ex)
        {
            // Banco indisponível no startup não derruba a API: o /health passa a reportar Unhealthy (503).
            app.Logger.LogCritical(ex, "Falha ao aplicar migrations/seed; a API sobe com o banco indisponível.");
        }

        // Configure the HTTP request pipeline.
        app.UseTraceIdLogging();
        app.UsePortalExceptionHandling();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            // Raiz sem rota: leva direto ao Swagger UI.
            app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        }

        app.UsePortalSwagger();

        // Em Development a API pode subir só em HTTP (perfil "http"); sem porta HTTPS o redirect só gera aviso.
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.MapPortalHealthChecks();

        app.Run();
    }
}
