﻿// <auto-generated />
using System;
using Barion.Balance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barion.Balance.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BalanceDbContext))]
    partial class BalanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Hold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalData")
                        .HasColumnType("jsonb");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int?>("HoldCaptureCreatedPaymentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCaptured")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVoided")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaidResourceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PaidResourceTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentSystemConfigurationId")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentSystemTransactionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PaymentSystemWidgetGenerationId")
                        .HasColumnType("integer");

                    b.Property<string>("ReceiptUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PaidResourceTypeId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("PaymentSystemConfigurationId");

                    b.ToTable("Holds");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaidResourceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaidResourceType");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalData")
                        .HasColumnType("jsonb");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("CapturedHoldId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("IsBonus")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaidResourceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PaidResourceTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentSystemConfigurationId")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentSystemTransactionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PaymentSystemWidgetId")
                        .HasColumnType("integer");

                    b.Property<string>("ReceiptUrl")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CapturedHoldId")
                        .IsUnique();

                    b.HasIndex("PaidResourceTypeId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("PaymentSystemConfigurationId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CardNumberData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CardType")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ExpiryMonth")
                        .HasColumnType("integer");

                    b.Property<int>("ExpiryYear")
                        .HasColumnType("integer");

                    b.Property<string>("First1")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("boolean");

                    b.Property<string>("Last4")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("PaymentSystemStamp")
                        .HasColumnType("text");

                    b.Property<string>("PaymentSystemToken")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaymentSystemConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<bool>("IsCurrentSchema")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("PaymentSystemName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentSystemConfigurations");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaymentSystemWidget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalData")
                        .HasColumnType("jsonb");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("GotResponseFromPaymentSystem")
                        .HasColumnType("boolean");

                    b.Property<int?>("HoldId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaidResourceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PaidResourceTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentSystemConfigurationId")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WidgetReason")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HoldId")
                        .IsUnique();

                    b.HasIndex("PaidResourceTypeId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("PaymentSystemConfigurationId");

                    b.ToTable("PaymentSystemWidget");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int?>("HoldId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReceiptForHold")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReceiptForPayment")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("PaidResourceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentSystemConfigurationId")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentSystemTransactionId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HoldId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("PaymentSystemConfigurationId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Hold", b =>
                {
                    b.HasOne("Barion.Balance.Domain.Entities.PaidResourceType", "PaidResourceType")
                        .WithMany("Holds")
                        .HasForeignKey("PaidResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("Holds")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentSystemConfiguration", "PaymentSystemConfiguration")
                        .WithMany("Holds")
                        .HasForeignKey("PaymentSystemConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaidResourceType");

                    b.Navigation("PaymentMethod");

                    b.Navigation("PaymentSystemConfiguration");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Payment", b =>
                {
                    b.HasOne("Barion.Balance.Domain.Entities.Hold", "CapturedHold")
                        .WithOne("HoldCaptureCreatedPayment")
                        .HasForeignKey("Barion.Balance.Domain.Entities.Payment", "CapturedHoldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaidResourceType", "PaidResourceType")
                        .WithMany("Payments")
                        .HasForeignKey("PaidResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentSystemConfiguration", "PaymentSystemConfiguration")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentSystemConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("CapturedHold");

                    b.Navigation("PaidResourceType");

                    b.Navigation("PaymentMethod");

                    b.Navigation("PaymentSystemConfiguration");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaymentSystemWidget", b =>
                {
                    b.HasOne("Barion.Balance.Domain.Entities.Hold", "Hold")
                        .WithOne("PaymentSystemWidget")
                        .HasForeignKey("Barion.Balance.Domain.Entities.PaymentSystemWidget", "HoldId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Barion.Balance.Domain.Entities.PaidResourceType", "PaidResourceType")
                        .WithMany("PaymentSystemWidgets")
                        .HasForeignKey("PaidResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.Payment", "Payment")
                        .WithOne("PaymentSystemWidgets")
                        .HasForeignKey("Barion.Balance.Domain.Entities.PaymentSystemWidget", "PaymentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentSystemConfiguration", "PaymentSystemConfiguration")
                        .WithMany("PaymentSystemWidgets")
                        .HasForeignKey("PaymentSystemConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Hold");

                    b.Navigation("PaidResourceType");

                    b.Navigation("Payment");

                    b.Navigation("PaymentSystemConfiguration");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Receipt", b =>
                {
                    b.HasOne("Barion.Balance.Domain.Entities.Hold", "Hold")
                        .WithMany("Receipts")
                        .HasForeignKey("HoldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.Payment", "Payment")
                        .WithMany("Receipts")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("Receipts")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barion.Balance.Domain.Entities.PaymentSystemConfiguration", "PaymentSystemConfiguration")
                        .WithMany("Receipts")
                        .HasForeignKey("PaymentSystemConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Hold");

                    b.Navigation("Payment");

                    b.Navigation("PaymentMethod");

                    b.Navigation("PaymentSystemConfiguration");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Hold", b =>
                {
                    b.Navigation("HoldCaptureCreatedPayment");

                    b.Navigation("PaymentSystemWidget");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaidResourceType", b =>
                {
                    b.Navigation("Holds");

                    b.Navigation("PaymentSystemWidgets");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.Payment", b =>
                {
                    b.Navigation("PaymentSystemWidgets");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaymentMethod", b =>
                {
                    b.Navigation("Holds");

                    b.Navigation("Payments");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("Barion.Balance.Domain.Entities.PaymentSystemConfiguration", b =>
                {
                    b.Navigation("Holds");

                    b.Navigation("PaymentSystemWidgets");

                    b.Navigation("Payments");

                    b.Navigation("Receipts");
                });
#pragma warning restore 612, 618
        }
    }
}
