using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCV.Entity.Migrations
{
    public partial class changedatetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "Institutions",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "Institutions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "Institutions",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "Institutions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
