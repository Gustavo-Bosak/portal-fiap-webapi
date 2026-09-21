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
        builder.Services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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

        await DatabaseSeeder.SeedAsync(app.Services);

        // Configure the HTTP request pipeline.
        app.UseTraceIdLogging();
        app.UsePortalExceptionHandling();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UsePortalSwagger();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapPortalHealthChecks();

        app.Run();
    }
}
