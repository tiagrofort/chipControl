using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChipControl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSimcardGerenciamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operadoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Cnpj = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OperadoraId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdentificacaoChip = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Iccid = table.Column<string>(type: "TEXT", maxLength: 22, nullable: false),
                    Ddd = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    PlanoTipo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TemMinutagem = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuantidadeMinutos = table.Column<int>(type: "INTEGER", nullable: true),
                    TemInternet = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuantidadeInternet = table.Column<int>(type: "INTEGER", nullable: true),
                    DataAquisicao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataAtivacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simcards_Operadoras_OperadoraId",
                        column: x => x.OperadoraId,
                        principalTable: "Operadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Simcards_Iccid",
                table: "Simcards",
                column: "Iccid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Simcards_OperadoraId_IdentificacaoChip",
                table: "Simcards",
                columns: new[] { "OperadoraId", "IdentificacaoChip" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Simcards");

            migrationBuilder.DropTable(
                name: "Operadoras");
        }
    }
}
