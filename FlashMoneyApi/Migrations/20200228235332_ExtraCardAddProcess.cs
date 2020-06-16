using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class ExtraCardAddProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "CardAddProcess",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CardId",
                table: "CardAddProcess",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CardAddProcess",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "CardAddProcess",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CardAddProcess",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CardAddProcess");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CardAddProcess");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CardAddProcess");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "CardAddProcess");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CardAddProcess");
        }
    }
}
