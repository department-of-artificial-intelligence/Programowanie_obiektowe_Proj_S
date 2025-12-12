using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PersonId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HotelId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PersonId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ResidentFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelRoomId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residents_HotelRooms_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Residents_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomHistoricResidents",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PersonId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ResidentFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResidentTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelRoomId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomHistoricResidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomHistoricResidents_HotelRooms_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoomHistoricResidents_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_HotelId",
                table: "HotelRooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ManagerId",
                table: "Hotels",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_PersonId",
                table: "Managers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_HotelRoomId",
                table: "Residents",
                column: "HotelRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_PersonId",
                table: "Residents",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomHistoricResidents_HotelRoomId",
                table: "RoomHistoricResidents",
                column: "HotelRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomHistoricResidents_PersonId",
                table: "RoomHistoricResidents",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "RoomHistoricResidents");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
