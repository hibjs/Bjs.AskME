using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AskME.Infrastructure.Migrations
{
    public partial class _20180418_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelfInfo",
                table: "UserInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "SelfInfo",
                table: "UserInfo");
        }
    }
}
