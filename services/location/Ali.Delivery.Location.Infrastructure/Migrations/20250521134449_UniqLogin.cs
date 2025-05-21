using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Location.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_locations_telegram_login",
                table: "userLocations",
                column: "telegram_login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_locations_telegram_login",
                table: "userLocations");
        }
    }
}
