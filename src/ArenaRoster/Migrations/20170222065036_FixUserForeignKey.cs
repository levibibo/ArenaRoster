using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaRoster.Migrations
{
    public partial class FixUserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_AspNetUsers_UserId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_UserId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Availabilities");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_AppUserId",
                table: "Availabilities",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_AspNetUsers_AppUserId",
                table: "Availabilities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_AspNetUsers_AppUserId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_AppUserId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Availabilities");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_UserId",
                table: "Availabilities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_AspNetUsers_UserId",
                table: "Availabilities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
