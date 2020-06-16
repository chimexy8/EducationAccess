using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class airtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Transfer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AirtimeRecharge",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DestinationPhone = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirtimeRecharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirtimeRecharge_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirtimeRecharge_UserId",
                table: "AirtimeRecharge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_UserId",
                table: "Withdrawal",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirtimeRecharge");

            migrationBuilder.DropTable(
                name: "Withdrawal");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Transfer");
        }
    }
}
