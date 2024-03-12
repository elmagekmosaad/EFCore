using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Facebook = table.Column<string>(type: "longtext", nullable: false, defaultValue: "807826532671834")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    Country = table.Column<string>(type: "longtext", nullable: false, defaultValue: "Egypt")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Admin = table.Column<string>(type: "longtext", nullable: false, defaultValue: "PC")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Hwid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Serial = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Period = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StartOfSubscription = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    EndOfSubscription = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 3, 12, 16, 42, 0, 0, DateTimeKind.Unspecified)),
                    Points = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false, defaultValue: 0m),
                    Discount = table.Column<decimal>(type: "decimal(6,2)", nullable: false, defaultValue: 0m),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ComputerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_CustomerId",
                table: "Computers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_Hwid",
                table: "Computers",
                column: "Hwid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_Serial",
                table: "Computers",
                column: "Serial");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_SubscriptionId",
                table: "Computers",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ComputerId",
                table: "Subscriptions",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CustomerId",
                table: "Subscriptions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Period_Points",
                table: "Subscriptions",
                columns: new[] { "Period", "Points" });

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Subscriptions_SubscriptionId",
                table: "Computers",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Customers_CustomerId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Customers_CustomerId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Subscriptions_SubscriptionId",
                table: "Computers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Computers");
        }
    }
}
