using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class otp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MothersMedianName",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CardNumber = table.Column<string>(nullable: true),
                    CardName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    CardExpMonth = table.Column<string>(nullable: true),
                    CardExpYear = table.Column<string>(nullable: true),
                    CardUrl = table.Column<string>(nullable: true),
                    CardRef = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    CardPIN = table.Column<string>(nullable: true),
                    TransID = table.Column<string>(nullable: true),
                    CardMessage = table.Column<string>(nullable: true),
                    CVV = table.Column<string>(nullable: true),
                    Amount = table.Column<string>(nullable: true),
                    Valid = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardDetail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OTPValidation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPValidation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    ReceiverId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Claimed = table.Column<bool>(nullable: false),
                    Narration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true),
                    TransactionReference = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    TransactionStatus = table.Column<int>(nullable: false),
                    IsAddCard = table.Column<bool>(nullable: false),
                    CardNumber = table.Column<string>(nullable: true),
                    CardName = table.Column<string>(nullable: true),
                    CardExpMonth = table.Column<string>(nullable: true),
                    CardExpYear = table.Column<string>(nullable: true),
                    CVV = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTransaction_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardDetail_UserId",
                table: "CardDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTransaction_UserId",
                table: "UserTransaction",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDetail");

            migrationBuilder.DropTable(
                name: "OTPValidation");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "UserTransaction");

            migrationBuilder.DropColumn(
                name: "MothersMedianName",
                table: "User");
        }
    }
}
