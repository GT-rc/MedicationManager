using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MedicationManager.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    SetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeOfDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.SetID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PermissionLevel = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    MedID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Dosage = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    RefillRate = table.Column<int>(nullable: false),
                    SetID = table.Column<int>(nullable: true),
                    TimeOfDay = table.Column<int>(nullable: false),
                    TimesXDay = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.MedID);
                    table.ForeignKey(
                        name: "FK_Medication_Set_SetID",
                        column: x => x.SetID,
                        principalTable: "Set",
                        principalColumn: "SetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medication_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedSets",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    SetId = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedSets", x => new { x.UserId, x.SetId });
                    table.ForeignKey(
                        name: "FK_MedSets_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "SetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedSets_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medication_SetID",
                table: "Medication",
                column: "SetID");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_UserId",
                table: "Medication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedSets_SetId",
                table: "MedSets",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_MedSets_UserId1",
                table: "MedSets",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "MedSets");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
