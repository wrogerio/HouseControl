using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseControl.Infra.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Lancamentos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,0)");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Lancamentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Lancamentos",
                type: "DECIMAL(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Lancamentos");
        }
    }
}
