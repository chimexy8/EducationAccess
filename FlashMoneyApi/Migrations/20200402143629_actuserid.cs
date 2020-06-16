using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class actuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityModel_User_UserId",
                table: "ActivityModel");

            migrationBuilder.DropIndex(
                name: "IX_ActivityModel_UserId",
                table: "ActivityModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ActivityModel",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ActivityModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityModel_UserId1",
                table: "ActivityModel",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityModel_User_UserId1",
                table: "ActivityModel",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityModel_User_UserId1",
                table: "ActivityModel");

            migrationBuilder.DropIndex(
                name: "IX_ActivityModel_UserId1",
                table: "ActivityModel");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ActivityModel");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ActivityModel",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityModel_UserId",
                table: "ActivityModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityModel_User_UserId",
                table: "ActivityModel",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
