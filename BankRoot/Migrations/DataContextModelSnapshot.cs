﻿// <auto-generated />
using System;
using BankRoot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankRoot.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("BankRoot.Models.Account", b =>
                {
                    b.Property<int>("Id_account")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id_account"));

                    b.Property<int>("Id_app_user")
                        .HasColumnType("integer");

                    b.Property<string>("account_number")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("account_status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("amount")
                        .HasColumnType("numeric");

                    b.HasKey("Id_account");

                    b.HasIndex("Id_app_user");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BankRoot.Models.App_user", b =>
                {
                    b.Property<int>("Id_app_user")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id_app_user"));

                    b.Property<int>("Id_role")
                        .HasColumnType("integer");

                    b.Property<string>("app_user_number")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_app_user");

                    b.HasIndex("Id_role");

                    b.ToTable("App_user");
                });

            modelBuilder.Entity("BankRoot.Models.Role", b =>
                {
                    b.Property<int>("Id_role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id_role"));

                    b.Property<string>("role_name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_role");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("BankRoot.Models.Transaction", b =>
                {
                    b.Property<int>("Id_transaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id_transaction"));

                    b.Property<int>("Ctransaction")
                        .HasColumnType("integer");

                    b.Property<int>("Dtransaction")
                        .HasColumnType("integer");

                    b.Property<decimal>("amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("date_transaction")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id_transaction");

                    b.HasIndex("Ctransaction");

                    b.HasIndex("Dtransaction");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BankRoot.Models.Account", b =>
                {
                    b.HasOne("BankRoot.Models.App_user", "App_user")
                        .WithMany("Accounts")
                        .HasForeignKey("Id_app_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App_user");
                });

            modelBuilder.Entity("BankRoot.Models.App_user", b =>
                {
                    b.HasOne("BankRoot.Models.Role", "Role")
                        .WithMany("App_users")
                        .HasForeignKey("Id_role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BankRoot.Models.Transaction", b =>
                {
                    b.HasOne("BankRoot.Models.Account", "AccountC")
                        .WithMany("TransactionsC")
                        .HasForeignKey("Ctransaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankRoot.Models.Account", "AccountD")
                        .WithMany("TransactionsD")
                        .HasForeignKey("Dtransaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountC");

                    b.Navigation("AccountD");
                });

            modelBuilder.Entity("BankRoot.Models.Account", b =>
                {
                    b.Navigation("TransactionsC");

                    b.Navigation("TransactionsD");
                });

            modelBuilder.Entity("BankRoot.Models.App_user", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BankRoot.Models.Role", b =>
                {
                    b.Navigation("App_users");
                });
#pragma warning restore 612, 618
        }
    }
}
