using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChipControl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperadoraGerenciamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Operadoras",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Operadoras");
        }
    }
}
