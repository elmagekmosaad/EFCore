using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureAndFrirstAndLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "security",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfSubscription",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 17, 19, 5, 29, 220, DateTimeKind.Local).AddTicks(9915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 17, 8, 46, 49, 80, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfSubscription",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 17, 19, 5, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 17, 8, 46, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndOfSubscription", "StartOfSubscription" },
                values: new object[] { new DateTime(2024, 4, 17, 19, 5, 29, 221, DateTimeKind.Local).AddTicks(6628), new DateTime(2024, 4, 17, 19, 5, 29, 221, DateTimeKind.Local).AddTicks(6611) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "security",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfSubscription",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 17, 8, 46, 49, 80, DateTimeKind.Local).AddTicks(9918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 17, 19, 5, 29, 220, DateTimeKind.Local).AddTicks(9915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfSubscription",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 17, 8, 46, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 17, 19, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndOfSubscription", "StartOfSubscription" },
                values: new object[] { new DateTime(2024, 4, 17, 8, 46, 49, 81, DateTimeKind.Local).AddTicks(6006), new DateTime(2024, 4, 17, 8, 46, 49, 81, DateTimeKind.Local).AddTicks(5993) });
        }
    }
}
