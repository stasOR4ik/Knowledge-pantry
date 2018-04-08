using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Knowledge_pantry.Data.Migrations
{
    public partial class Addsummarieslisttouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Summaries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_ApplicationUserId",
                table: "Summaries",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Summaries_AspNetUsers_ApplicationUserId",
                table: "Summaries",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Summaries_AspNetUsers_ApplicationUserId",
                table: "Summaries");

            migrationBuilder.DropIndex(
                name: "IX_Summaries_ApplicationUserId",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Summaries");
        }
    }
}
