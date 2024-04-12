using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barion.Balance.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PaymentMethods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Holds",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccountRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Holds");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccountRecords");
        }
    }
}
