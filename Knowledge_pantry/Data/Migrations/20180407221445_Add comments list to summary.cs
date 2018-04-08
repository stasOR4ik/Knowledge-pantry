using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Knowledge_pantry.Data.Migrations
{
    public partial class Addcommentslisttosummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummaryLink",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "SummaryId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SummaryId",
                table: "Comments",
                column: "SummaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Summaries_SummaryId",
                table: "Comments",
                column: "SummaryId",
                principalTable: "Summaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Summaries_SummaryId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SummaryId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SummaryId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "SummaryLink",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
