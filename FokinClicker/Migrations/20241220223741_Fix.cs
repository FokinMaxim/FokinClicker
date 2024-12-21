using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FokinClicker.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "UserSupports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoostId",
                table: "Supports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSupports_ApplicationUserId",
                table: "UserSupports",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSupports_AspNetUsers_ApplicationUserId",
                table: "UserSupports",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSupports_AspNetUsers_ApplicationUserId",
                table: "UserSupports");

            migrationBuilder.DropIndex(
                name: "IX_UserSupports_ApplicationUserId",
                table: "UserSupports");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserSupports");

            migrationBuilder.DropColumn(
                name: "BoostId",
                table: "Supports");
        }
    }
}
