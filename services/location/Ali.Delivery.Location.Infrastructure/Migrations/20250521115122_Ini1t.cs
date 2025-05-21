using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Location.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ini1t : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userLocations",
                table: "userLocations");

            migrationBuilder.RenameColumn(
                name: "S",
                table: "userLocations",
                newName: "s");

            migrationBuilder.RenameColumn(
                name: "E",
                table: "userLocations",
                newName: "e");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "userLocations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "userLocations",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "userLocations",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "TelegramLogin",
                table: "userLocations",
                newName: "telegram_login");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "userLocations",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "userLocations",
                newName: "created_by");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_locations",
                table: "userLocations",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_locations",
                table: "userLocations");

            migrationBuilder.RenameColumn(
                name: "s",
                table: "userLocations",
                newName: "S");

            migrationBuilder.RenameColumn(
                name: "e",
                table: "userLocations",
                newName: "E");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "userLocations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "userLocations",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "userLocations",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "telegram_login",
                table: "userLocations",
                newName: "TelegramLogin");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "userLocations",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "userLocations",
                newName: "CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userLocations",
                table: "userLocations",
                column: "Id");
        }
    }
}
