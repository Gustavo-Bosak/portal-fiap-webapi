using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence;

public class PortalFiapContext : DbContext
{
    public PortalFiapContext(DbContextOptions<PortalFiapContext> options) : base(options)
    {
    }
    
    public DbSet<Aluno> Alunos { get; set; }
    
    public DbSet<Bolsa> Bolsas { get; set; }
    
    public DbSet<Curso> Cursos { get; set; }
    
    public DbSet<Endereco> Enderecos { get; set; }
    
    public DbSet<Matricula> Matriculas { get; set; }
    
    public DbSet<Professor> Professores { get; set; }
    
    public DbSet<Turma> Turmas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalFiapContext).Assembly);

        modelBuilder.Entity<Aluno>().UseTpcMappingStrategy();
        modelBuilder.Entity<Professor>().UseTpcMappingStrategy();
    }
}