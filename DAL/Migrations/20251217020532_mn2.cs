using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class mn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_ServiceId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Overall",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Services",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Ratings",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ServiceId",
                table: "Ratings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ServiceId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Services",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Ratings",
                newName: "Date");

            migrationBuilder.AddColumn<float>(
                name: "Overall",
                table: "Services",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Services",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Quality",
                table: "Services",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ServiceId",
                table: "Ratings",
                column: "ServiceId",
                unique: true);
        }
    }
}
