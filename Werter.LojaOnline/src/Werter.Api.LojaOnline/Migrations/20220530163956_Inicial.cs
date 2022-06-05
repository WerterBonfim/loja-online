using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Werter.Api.LojaOnline.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    QuantidadeEmEstoque = table.Column<int>(type: "int", nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraAlterado = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
