using Microsoft.EntityFrameworkCore;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFIAP.Application.Services;
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

        builder.Services.AddControllers();

        
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
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        await DatabaseSeeder.SeedAsync(app.Services);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

        app.Run();
    }
}