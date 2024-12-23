using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FokinClicker.Migrations
{
    /// <inheritdoc />
    public partial class UsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoosts_AspNetUsers_ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSupports_AspNetUsers_ApplicationUserId",
                table: "UserSupports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSupports_Boosts_UserBoostId",
                table: "UserSupports");

            migrationBuilder.DropIndex(
                name: "IX_UserSupports_ApplicationUserId",
                table: "UserSupports");

            migrationBuilder.DropIndex(
                name: "IX_UserSupports_UserBoostId",
                table: "UserSupports");

            migrationBuilder.DropIndex(
                name: "IX_UserBoosts_ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserSupports");

            migrationBuilder.DropColumn(
                name: "UserBoostId",
                table: "UserSupports");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserBoosts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "UserSupports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserBoostId",
                table: "UserSupports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "UserBoosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSupports_ApplicationUserId",
                table: "UserSupports",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSupports_UserBoostId",
                table: "UserSupports",
                column: "UserBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoosts_ApplicationUserId",
                table: "UserBoosts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoosts_AspNetUsers_ApplicationUserId",
                table: "UserBoosts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSupports_AspNetUsers_ApplicationUserId",
                table: "UserSupports",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSupports_Boosts_UserBoostId",
                table: "UserSupports",
                column: "UserBoostId",
                principalTable: "Boosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
