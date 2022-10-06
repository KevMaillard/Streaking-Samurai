using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankRoot.Migrations
{
    public partial class Idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id_role = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    role_name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id_role);
                });

            migrationBuilder.CreateTable(
                name: "App_user",
                columns: table => new
                {
                    Id_app_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    app_user_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    Id_role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_user", x => x.Id_app_user);
                    table.ForeignKey(
                        name: "FK_App_user_Role_Id_role",
                        column: x => x.Id_role,
                        principalTable: "Role",
                        principalColumn: "Id_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id_account = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    account_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    account_status = table.Column<string>(type: "varchar(50)", nullable: false),
                    Id_app_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id_account);
                    table.ForeignKey(
                        name: "FK_Account_App_user_Id_app_user",
                        column: x => x.Id_app_user,
                        principalTable: "App_user",
                        principalColumn: "Id_app_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id_transaction = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Dtransaction = table.Column<int>(type: "integer", nullable: false),
                    Ctransaction = table.Column<int>(type: "integer", nullable: false),
                    date_transaction = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id_transaction);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_Ctransaction",
                        column: x => x.Ctransaction,
                        principalTable: "Account",
                        principalColumn: "Id_account",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_Dtransaction",
                        column: x => x.Dtransaction,
                        principalTable: "Account",
                        principalColumn: "Id_account",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Id_app_user",
                table: "Account",
                column: "Id_app_user");

            migrationBuilder.CreateIndex(
                name: "IX_App_user_Id_role",
                table: "App_user",
                column: "Id_role");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Ctransaction",
                table: "Transaction",
                column: "Ctransaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Dtransaction",
                table: "Transaction",
                column: "Dtransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "App_user");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
