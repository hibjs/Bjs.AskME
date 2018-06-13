using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AskME.Infrastructure.Migrations
{
    public partial class _20180416_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    SubTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolg_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagFollower",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagFollower", x => new { x.TagId, x.UserInfoId });
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BolgTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    BlogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolgTag", x => new { x.TagId, x.BlogId });
                    table.ForeignKey(
                        name: "FK_BolgTag_Bolg_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Bolg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BolgTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolg_UserInfoId",
                table: "Bolg",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BolgTag_BlogId",
                table: "BolgTag",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_TagFollower_UserInfoId",
                table: "TagFollower",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BolgTag");

            migrationBuilder.DropTable(
                name: "TagFollower");

            migrationBuilder.DropTable(
                name: "Bolg");
        }
    }
}
