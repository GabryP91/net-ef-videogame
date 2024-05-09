using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_ef_videogame.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Software_house",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_house", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Videogame",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Release = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Software_houseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogame", x => x.id);
                    table.ForeignKey(
                        name: "FK_Videogame_Software_house_Software_houseid",
                        column: x => x.Software_houseid,
                        principalTable: "Software_house",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Software_house_Tax_id",
                table: "Software_house",
                column: "Tax_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videogame_Name",
                table: "Videogame",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videogame_Software_houseid",
                table: "Videogame",
                column: "Software_houseid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videogame");

            migrationBuilder.DropTable(
                name: "Software_house");
        }
    }
}
