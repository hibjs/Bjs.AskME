using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AskME.Infrastructure.Migrations
{
    public partial class _20180416_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Question_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_UserInfo_UserInfoId",
                table: "Tag");

            migrationBuilder.DropTable(
                name: "Commont");

            migrationBuilder.DropTable(
                name: "TagFollower");

            migrationBuilder.DropIndex(
                name: "IX_Tag_UserInfoId",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "Anonymous",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Anonymous",
                table: "Answer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "QuestionTags",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags",
                columns: new[] { "TagId", "QuestionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Question_QuestionId",
                table: "QuestionTags",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Question_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Tag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "QuestionTags",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuestionTags",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<bool>(
                name: "Anonymous",
                table: "Question",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Anonymous",
                table: "Answer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Commont",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    SubTime = table.Column<DateTime>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commont", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commont_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commont_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagFollower",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubTime = table.Column<DateTime>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagFollower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagFollower_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagFollower_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UserInfoId",
                table: "Tag",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Commont_AnswerId",
                table: "Commont",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Commont_UserInfoId",
                table: "Commont",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TagFollower_TagId",
                table: "TagFollower",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagFollower_UserInfoId",
                table: "TagFollower",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Question_QuestionId",
                table: "QuestionTags",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_UserInfo_UserInfoId",
                table: "Tag",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
