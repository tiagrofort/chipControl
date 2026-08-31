using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChipControl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Login = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    NivelAcesso = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSistema", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_Login",
                table: "UsuariosSistema",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosSistema");
        }
    }
}
