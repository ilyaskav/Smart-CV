using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCV.Entity.Migrations
{
    public partial class dataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContactTitles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "EMail" },
                    { 2, "Phone" },
                    { 3, "Сайт" },
                    { 4, "LinkedIn" }
                });

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Name", "Rules" },
                values: new object[,]
                {
                    { 1, "IT", "1" },
                    { 2, "Спорт", "2" },
                    { 3, "Менеджмент", "3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactTitles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContactTitles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContactTitles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ContactTitles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Professions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
