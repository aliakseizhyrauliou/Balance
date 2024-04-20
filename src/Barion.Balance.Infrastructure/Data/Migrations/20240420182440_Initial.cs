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
                name: "PaidResourceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidResourceType", x => x.Id);
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
                    CardNumberData = table.Column<string>(type: "text", nullable: false),
                    CardType = table.Column<int>(type: "integer", nullable: false),
                    ExpiryYear = table.Column<int>(type: "integer", nullable: false),
                    ExpiryMonth = table.Column<int>(type: "integer", nullable: false),
                    IsSelected = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentSystemStamp = table.Column<string>(type: "text", nullable: true),
                    PaymentSystemToken = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSystemConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentSystemName = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "jsonb", nullable: false),
                    IsCurrentSchema = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSystemConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PaidResourceTypeId = table.Column<int>(type: "integer", nullable: true),
                    PaidResourceId = table.Column<string>(type: "text", nullable: false),
                    PaymentSystemTransactionId = table.Column<string>(type: "text", nullable: false),
                    OperatorId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsCaptured = table.Column<bool>(type: "boolean", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    AdditionalData = table.Column<string>(type: "jsonb", nullable: true),
                    PaymentSystemConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    ReceiptUrl = table.Column<string>(type: "text", nullable: false),
                    HoldCaptureCreatedPaymentId = table.Column<int>(type: "integer", nullable: true),
                    PaymentSystemWidgetGenerationId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holds_PaidResourceType_PaidResourceTypeId",
                        column: x => x.PaidResourceTypeId,
                        principalTable: "PaidResourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holds_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holds_PaymentSystemConfigurations_PaymentSystemConfiguratio~",
                        column: x => x.PaymentSystemConfigurationId,
                        principalTable: "PaymentSystemConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidResourceId = table.Column<string>(type: "text", nullable: false),
                    PaymentSystemTransactionId = table.Column<string>(type: "text", nullable: false),
                    AdditionalData = table.Column<string>(type: "jsonb", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    IsBonus = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    PaidResourceTypeId = table.Column<int>(type: "integer", nullable: true),
                    PaymentSystemConfigurationId = table.Column<int>(type: "integer", nullable: true),
                    ReceiptUrl = table.Column<string>(type: "text", nullable: true),
                    CapturedHoldId = table.Column<int>(type: "integer", nullable: true),
                    PaymentSystemWidgetId = table.Column<int>(type: "integer", nullable: true),
                    CaptureDebtorId = table.Column<int>(type: "integer", nullable: true),
                    IsRefund = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Holds_CapturedHoldId",
                        column: x => x.CapturedHoldId,
                        principalTable: "Holds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaidResourceType_PaidResourceTypeId",
                        column: x => x.PaidResourceTypeId,
                        principalTable: "PaidResourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentSystemConfigurations_PaymentSystemConfigura~",
                        column: x => x.PaymentSystemConfigurationId,
                        principalTable: "PaymentSystemConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Debtors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    PaidResourceTypeId = table.Column<int>(type: "integer", nullable: true),
                    PaymentSystemConfigurationId = table.Column<int>(type: "integer", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: false),
                    PaidResourceId = table.Column<string>(type: "text", nullable: true),
                    NewPaymentId = table.Column<int>(type: "integer", nullable: true),
                    AdditionalData = table.Column<string>(type: "text", nullable: true),
                    CaptureAttemptCount = table.Column<int>(type: "integer", nullable: false),
                    LastCaptureAttempt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debtors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debtors_PaidResourceType_PaidResourceTypeId",
                        column: x => x.PaidResourceTypeId,
                        principalTable: "PaidResourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debtors_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debtors_PaymentSystemConfigurations_PaymentSystemConfigurat~",
                        column: x => x.PaymentSystemConfigurationId,
                        principalTable: "PaymentSystemConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debtors_Payments_NewPaymentId",
                        column: x => x.NewPaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSystemWidget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    WidgetReason = table.Column<int>(type: "integer", nullable: false),
                    PaidResourceId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    GotResponseFromPaymentSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    PaymentSystemConfigurationId = table.Column<int>(type: "integer", nullable: true),
                    PaidResourceTypeId = table.Column<int>(type: "integer", nullable: true),
                    HoldId = table.Column<int>(type: "integer", nullable: true),
                    PaymentId = table.Column<int>(type: "integer", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: false),
                    AdditionalData = table.Column<string>(type: "jsonb", nullable: true),
                    PaymentSystemMessage = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSystemWidget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSystemWidget_Holds_HoldId",
                        column: x => x.HoldId,
                        principalTable: "Holds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentSystemWidget_PaidResourceType_PaidResourceTypeId",
                        column: x => x.PaidResourceTypeId,
                        principalTable: "PaidResourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentSystemWidget_PaymentSystemConfigurations_PaymentSyst~",
                        column: x => x.PaymentSystemConfigurationId,
                        principalTable: "PaymentSystemConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentSystemWidget_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PaymentSystemTransactionId = table.Column<string>(type: "text", nullable: true),
                    PaidResourceId = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    PaymentSystemConfigurationId = table.Column<int>(type: "integer", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    PaymentId = table.Column<int>(type: "integer", nullable: true),
                    HoldId = table.Column<int>(type: "integer", nullable: true),
                    PaidResourceTypeId = table.Column<int>(type: "integer", nullable: true),
                    IsReceiptForHold = table.Column<bool>(type: "boolean", nullable: false),
                    IsReceiptForPayment = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Holds_HoldId",
                        column: x => x.HoldId,
                        principalTable: "Holds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_PaidResourceType_PaidResourceTypeId",
                        column: x => x.PaidResourceTypeId,
                        principalTable: "PaidResourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_PaymentSystemConfigurations_PaymentSystemConfigura~",
                        column: x => x.PaymentSystemConfigurationId,
                        principalTable: "PaymentSystemConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_NewPaymentId",
                table: "Debtors",
                column: "NewPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_PaidResourceTypeId",
                table: "Debtors",
                column: "PaidResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_PaymentMethodId",
                table: "Debtors",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_PaymentSystemConfigurationId",
                table: "Debtors",
                column: "PaymentSystemConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_PaidResourceTypeId",
                table: "Holds",
                column: "PaidResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_PaymentMethodId",
                table: "Holds",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_PaymentSystemConfigurationId",
                table: "Holds",
                column: "PaymentSystemConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CapturedHoldId",
                table: "Payments",
                column: "CapturedHoldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaidResourceTypeId",
                table: "Payments",
                column: "PaidResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentSystemConfigurationId",
                table: "Payments",
                column: "PaymentSystemConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSystemWidget_HoldId",
                table: "PaymentSystemWidget",
                column: "HoldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSystemWidget_PaidResourceTypeId",
                table: "PaymentSystemWidget",
                column: "PaidResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSystemWidget_PaymentId",
                table: "PaymentSystemWidget",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSystemWidget_PaymentSystemConfigurationId",
                table: "PaymentSystemWidget",
                column: "PaymentSystemConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_HoldId",
                table: "Receipts",
                column: "HoldId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PaidResourceTypeId",
                table: "Receipts",
                column: "PaidResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PaymentId",
                table: "Receipts",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PaymentMethodId",
                table: "Receipts",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PaymentSystemConfigurationId",
                table: "Receipts",
                column: "PaymentSystemConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Debtors");

            migrationBuilder.DropTable(
                name: "PaymentSystemWidget");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropTable(
                name: "PaidResourceType");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentSystemConfigurations");
        }
    }
}
