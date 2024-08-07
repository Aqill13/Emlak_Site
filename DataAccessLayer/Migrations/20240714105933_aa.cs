using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoritesId",
                table: "Adverts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_FavoritesId",
                table: "Adverts",
                column: "FavoritesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Favorites_FavoritesId",
                table: "Adverts",
                column: "FavoritesId",
                principalTable: "Favorites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Favorites_FavoritesId",
                table: "Adverts");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_FavoritesId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "FavoritesId",
                table: "Adverts");
        }
    }
}
