using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AskME.Infrastructure.Migrations
{
    public partial class _20180417_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Down",
                table: "Question",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Up",
                table: "Question",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Down",
                table: "Answer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Up",
                table: "Answer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Down",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Down",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "Answer");
        }
    }
}
