using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalFiap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaDataNascimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DataNascimento",
                table: "CP_Professores",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataNascimento",
                table: "CP_Alunos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "CP_Professores");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "CP_Alunos");
        }
    }
}
