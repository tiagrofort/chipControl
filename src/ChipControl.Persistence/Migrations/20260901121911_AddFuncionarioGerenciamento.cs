using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChipControl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFuncionarioGerenciamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCompleto = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Matricula = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Setor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TelefonePessoal = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
