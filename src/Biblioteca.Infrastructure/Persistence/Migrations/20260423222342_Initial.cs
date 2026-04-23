using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    autor = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ano_publicacao = table.Column<int>(type: "integer", nullable: false),
                    quantidade_disponivel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emprestimos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    livro_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_emprestimo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_devolucao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emprestimos", x => x.id);
                    table.ForeignKey(
                        name: "FK_emprestimos_livros_livro_id",
                        column: x => x.livro_id,
                        principalTable: "livros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emprestimos_livro_id",
                table: "emprestimos",
                column: "livro_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emprestimos");

            migrationBuilder.DropTable(
                name: "livros");
        }
    }
}
