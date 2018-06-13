using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AskME.Infrastructure.Migrations
{
    public partial class _20180415_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Answer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserInfoId",
                table: "Answer",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_UserInfo_UserInfoId",
                table: "Answer",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_UserInfo_UserInfoId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserInfoId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Answer");
        }
    }
}
