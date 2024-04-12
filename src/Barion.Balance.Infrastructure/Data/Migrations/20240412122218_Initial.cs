using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barion.Balance.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TagId = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidResourceId = table.Column<int>(type: "integer", nullable: false),
                    PaymentSystemFinancialTransactionId = table.Column<string>(type: "text", nullable: false),
                    PaidResourceType = table.Column<int>(type: "integer", nullable: false),
                    ConnectorId = table.Column<int>(type: "integer", nullable: true),
                    ChargerId = table.Column<int>(type: "integer", nullable: true),
                    TariffId = table.Column<int>(type: "integer", nullable: true),
                    OperatorId = table.Column<int>(type: "integer", nullable: true),
                    IsSuccessfully = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    Last4 = table.Column<string>(type: "text", nullable: true),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PaidResourceId = table.Column<int>(type: "integer", nullable: true),
                    PaymentSystemTransactionId = table.Column<string>(type: "text", nullable: true),
                    OperatorId = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsCaptured = table.Column<bool>(type: "boolean", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    PaidResourceType = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    First1 = table.Column<string>(type: "text", nullable: true),
                    Last4 = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<int>(type: "integer", nullable: false),
                    ExpiryYear = table.Column<string>(type: "text", nullable: true),
                    ExpiryMonth = table.Column<string>(type: "text", nullable: true),
                    IsSelected = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentSystemStamp = table.Column<string>(type: "text", nullable: true),
                    PaymentSystemToken = table.Column<string>(type: "text", nullable: true),
                    IsVerifiedByPaymentSystem = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRecords");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropTable(
                name: "PaymentMethods");
        }
    }
}
