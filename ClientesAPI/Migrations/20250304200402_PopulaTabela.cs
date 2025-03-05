using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientesAPI.Migrations
{
    public partial class PopulaTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Idade", "Nome" },
                values: new object[] { 1, "joaosilva@dominio.com", 30, "João Silva" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Idade", "Nome" },
                values: new object[] { 2, "mariaaraujo@dominio.com", 25, "Maria Araujo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
