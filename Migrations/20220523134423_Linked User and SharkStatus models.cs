using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharkSpotterAPI.Migrations
{
    public partial class LinkedUserandSharkStatusmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SharkStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SharkStatuses_UserId",
                table: "SharkStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharkStatuses_Users_UserId",
                table: "SharkStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharkStatuses_Users_UserId",
                table: "SharkStatuses");

            migrationBuilder.DropIndex(
                name: "IX_SharkStatuses_UserId",
                table: "SharkStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SharkStatuses");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");
        }
    }
}
