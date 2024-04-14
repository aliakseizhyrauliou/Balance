﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barion.Balance.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "PaymentSystemWidgetGenerations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "PaymentSystemWidgetGenerations");
        }
    }
}
