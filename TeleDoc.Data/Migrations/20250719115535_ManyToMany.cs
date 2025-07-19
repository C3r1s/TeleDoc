using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleDoc.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegalEntityId",
                table: "Founders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LegalEntityId",
                table: "Founders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
