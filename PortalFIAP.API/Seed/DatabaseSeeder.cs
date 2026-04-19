using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;
using PortalFiap.Infrastructure.Persistence;

namespace PortalFiap.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PortalFiapContext>();

        await context.Database.EnsureCreatedAsync();

        if (await context.Alunos.AnyAsync())
            return;

        var endereco1 = new Endereco("Av. Paulista, 1000", "SP", "São Paulo", "Bela Vista", "01310100");
        var endereco2 = new Endereco("Rua Augusta, 500", "SP", "São Paulo", "Consolação", "01304000");
        var enderecoProfessor = new Endereco("Rua Vergueiro, 200", "SP", "São Paulo", "Liberdade", "01504000");

        var curso = new Curso(NomeCurso.AnaliseEDesenvolvimentoDeSistemas, 2400);

        var turma = new Turma("ADS-2026-1", 2026, 4, curso, new List<Matricula>(), new List<Professor>());

        var aluno1 = new Aluno(
            "João Silva",
            "joao.silva@fiap.com.br",
            new DateOnly(2000, 3, 15),
            "11999990001",
            endereco1,
            new List<Matricula>());

        var aluno2 = new Aluno(
            "Maria Oliveira",
            "maria.oliveira@fiap.com.br",
            new DateOnly(2001, 7, 22),
            "11999990002",
            endereco2,
            new List<Matricula>());

        var professor = new Professor(
            "Carlos Santos",
            "carlos.santos@fiap.com.br",
            new DateOnly(1985, 1, 10),
            "11999990003",
            enderecoProfessor,
            new List<Turma> { turma });

        turma.Professores.Add(professor);

        var matricula1 = new Matricula(aluno1, turma, null);
        var matricula2 = new Matricula(aluno2, turma, null);

        context.Enderecos.AddRange(endereco1, endereco2, enderecoProfessor);
        context.Cursos.Add(curso);
        context.Turmas.Add(turma);
        context.Alunos.AddRange(aluno1, aluno2);
        context.Professores.Add(professor);
        context.Matriculas.AddRange(matricula1, matricula2);

        await context.SaveChangesAsync();
    }
}
