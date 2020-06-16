using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class nextofkin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityBill",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NextOfKin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_UserId",
                table: "NextOfKin",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NextOfKin");

            migrationBuilder.DropColumn(
                name: "Passport",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UtilityBill",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ValidId",
                table: "User");
        }
    }
}
