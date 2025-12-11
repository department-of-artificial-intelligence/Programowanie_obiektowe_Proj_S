using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.DATABASE.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CinemaAddress",
                columns: table => new
                {
                    CinemaAddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaAddress", x => x.CinemaAddressID);
                });

            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    CinemaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinemaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaAddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.CinemaID);
                    table.ForeignKey(
                        name: "FK_Cinemas_CinemaAddress_CinemaAddressID",
                        column: x => x.CinemaAddressID,
                        principalTable: "CinemaAddress",
                        principalColumn: "CinemaAddressID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinemaID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Cinemas_CinemaID",
                        column: x => x.CinemaID,
                        principalTable: "Cinemas",
                        principalColumn: "CinemaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    CinemaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Halls_Cinemas_CinemaID",
                        column: x => x.CinemaID,
                        principalTable: "Cinemas",
                        principalColumn: "CinemaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeMin = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HallID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Films_Halls_HallID",
                        column: x => x.HallID,
                        principalTable: "Halls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CinemaAddressID",
                table: "Cinemas",
                column: "CinemaAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CinemaID",
                table: "Employees",
                column: "CinemaID");

            migrationBuilder.CreateIndex(
                name: "IX_Films_HallID",
                table: "Films",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CinemaID",
                table: "Halls",
                column: "CinemaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DropTable(
                name: "CinemaAddress");
        }
    }
}
