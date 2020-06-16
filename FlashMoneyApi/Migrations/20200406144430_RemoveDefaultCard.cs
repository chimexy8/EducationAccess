﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashMoneyApi.Migrations
{
    public partial class RemoveDefaultCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "CardDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "CardDetail",
                nullable: false,
                defaultValue: false);
        }
    }
}
