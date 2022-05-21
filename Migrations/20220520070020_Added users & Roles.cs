using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharkSpotterAPI.Migrations
{
    public partial class AddedusersRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sharkStatuses_Beaches_BeachId",
                table: "sharkStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_sharkStatuses_Flags_FlagId",
                table: "sharkStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sharkStatuses",
                table: "sharkStatuses");

            migrationBuilder.RenameTable(
                name: "sharkStatuses",
                newName: "SharkStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_sharkStatuses_FlagId",
                table: "SharkStatuses",
                newName: "IX_SharkStatuses_FlagId");

            migrationBuilder.RenameIndex(
                name: "IX_sharkStatuses_BeachId",
                table: "SharkStatuses",
                newName: "IX_SharkStatuses_BeachId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharkStatuses",
                table: "SharkStatuses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharkStatuses_Beaches_BeachId",
                table: "SharkStatuses",
                column: "BeachId",
                principalTable: "Beaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharkStatuses_Flags_FlagId",
                table: "SharkStatuses",
                column: "FlagId",
                principalTable: "Flags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharkStatuses_Beaches_BeachId",
                table: "SharkStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SharkStatuses_Flags_FlagId",
                table: "SharkStatuses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharkStatuses",
                table: "SharkStatuses");

            migrationBuilder.RenameTable(
                name: "SharkStatuses",
                newName: "sharkStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_SharkStatuses_FlagId",
                table: "sharkStatuses",
                newName: "IX_sharkStatuses_FlagId");

            migrationBuilder.RenameIndex(
                name: "IX_SharkStatuses_BeachId",
                table: "sharkStatuses",
                newName: "IX_sharkStatuses_BeachId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sharkStatuses",
                table: "sharkStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sharkStatuses_Beaches_BeachId",
                table: "sharkStatuses",
                column: "BeachId",
                principalTable: "Beaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sharkStatuses_Flags_FlagId",
                table: "sharkStatuses",
                column: "FlagId",
                principalTable: "Flags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
