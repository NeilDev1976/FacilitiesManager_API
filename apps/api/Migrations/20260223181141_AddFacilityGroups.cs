using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilitiesManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilityGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Facilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacilityGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_GroupId",
                table: "Facilities",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_FacilityGroups_GroupId",
                table: "Facilities",
                column: "GroupId",
                principalTable: "FacilityGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilityGroups_GroupId",
                table: "Facilities");

            migrationBuilder.DropTable(
                name: "FacilityGroups");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_GroupId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Facilities");
        }
    }
}
