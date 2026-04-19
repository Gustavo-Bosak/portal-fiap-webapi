using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalFiap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CP_Cursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CargaHoraria = table.Column<int>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CP_Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Logradouro = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CP_Turmas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeTurma = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AnoLetivo = table.Column<int>(type: "INTEGER", nullable: false),
                    Semestre = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCurso = table.Column<Guid>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CP_Turmas_CP_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "CP_Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CP_Alunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdEndereco = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CP_Alunos_CP_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "CP_Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CP_Professores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdEndereco = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Professores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CP_Professores_CP_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "CP_Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CP_Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdAluno = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdTurma = table.Column<Guid>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CP_Matriculas_CP_Alunos_IdAluno",
                        column: x => x.IdAluno,
                        principalTable: "CP_Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CP_Matriculas_CP_Turmas_IdTurma",
                        column: x => x.IdTurma,
                        principalTable: "CP_Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CP_Turma_Professores",
                columns: table => new
                {
                    ProfessoresId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TurmasId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Turma_Professores", x => new { x.ProfessoresId, x.TurmasId });
                    table.ForeignKey(
                        name: "FK_CP_Turma_Professores_CP_Professores_ProfessoresId",
                        column: x => x.ProfessoresId,
                        principalTable: "CP_Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CP_Turma_Professores_CP_Turmas_TurmasId",
                        column: x => x.TurmasId,
                        principalTable: "CP_Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CP_Bolsas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdMatricula = table.Column<Guid>(type: "TEXT", nullable: false),
                    Desconto = table.Column<decimal>(type: "TEXT", precision: 5, scale: 4, nullable: false),
                    Validade = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_Bolsas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CP_Bolsas_CP_Matriculas_IdMatricula",
                        column: x => x.IdMatricula,
                        principalTable: "CP_Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CP_Alunos_IdEndereco",
                table: "CP_Alunos",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_CP_Bolsas_IdMatricula",
                table: "CP_Bolsas",
                column: "IdMatricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CP_Matriculas_IdAluno",
                table: "CP_Matriculas",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_CP_Matriculas_IdTurma",
                table: "CP_Matriculas",
                column: "IdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_CP_Professores_IdEndereco",
                table: "CP_Professores",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_CP_Turma_Professores_TurmasId",
                table: "CP_Turma_Professores",
                column: "TurmasId");

            migrationBuilder.CreateIndex(
                name: "IX_CP_Turmas_IdCurso",
                table: "CP_Turmas",
                column: "IdCurso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CP_Bolsas");

            migrationBuilder.DropTable(
                name: "CP_Turma_Professores");

            migrationBuilder.DropTable(
                name: "CP_Matriculas");

            migrationBuilder.DropTable(
                name: "CP_Professores");

            migrationBuilder.DropTable(
                name: "CP_Alunos");

            migrationBuilder.DropTable(
                name: "CP_Turmas");

            migrationBuilder.DropTable(
                name: "CP_Enderecos");

            migrationBuilder.DropTable(
                name: "CP_Cursos");
        }
    }
}
