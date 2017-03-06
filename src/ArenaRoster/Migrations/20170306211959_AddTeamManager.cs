using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaRoster.Migrations
{
    public partial class AddTeamManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamManagerId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_TeamManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamManagerId",
                table: "Teams");
        }
    }
}
