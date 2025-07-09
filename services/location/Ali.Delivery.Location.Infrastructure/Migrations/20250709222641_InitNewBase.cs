using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Location.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitNewBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_locations_telegram_login",
                table: "userLocations");

            migrationBuilder.DropColumn(
                name: "telegram_login",
                table: "userLocations");

            migrationBuilder.RenameTable(
                name: "userLocations",
                newName: "user_locations");

            migrationBuilder.AlterColumn<string>(
                name: "s",
                table: "user_locations",
                type: "text",
                nullable: true,
                comment: "Координаты широты",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Координаты S");

            migrationBuilder.AlterColumn<string>(
                name: "e",
                table: "user_locations",
                type: "text",
                nullable: true,
                comment: "Координаты долготы",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Координаты E");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "user_locations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    chat_id = table.Column<string>(type: "text", nullable: false, comment: "ID чата пользователя из телеграма"),
                    login = table.Column<string>(type: "text", nullable: false, comment: "Логин пользователя"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                },
                comment: "Пользователи");

            migrationBuilder.CreateTable(
                name: "user_config",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    language = table.Column<string>(type: "text", nullable: false, comment: "Язык интерфейса пользователя"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_config", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_config_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Конфигурации пользователя");

            migrationBuilder.CreateIndex(
                name: "ix_user_locations_user_id",
                table: "user_locations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_config_user_id",
                table: "user_config",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_chat_id",
                table: "users",
                column: "chat_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_user_locations_users_user_id",
                table: "user_locations",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_locations_users_user_id",
                table: "user_locations");

            migrationBuilder.DropTable(
                name: "user_config");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "ix_user_locations_user_id",
                table: "user_locations");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_locations");

            migrationBuilder.RenameTable(
                name: "user_locations",
                newName: "userLocations");

            migrationBuilder.AlterColumn<string>(
                name: "s",
                table: "userLocations",
                type: "text",
                nullable: true,
                comment: "Координаты S",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Координаты широты");

            migrationBuilder.AlterColumn<string>(
                name: "e",
                table: "userLocations",
                type: "text",
                nullable: true,
                comment: "Координаты E",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Координаты долготы");

            migrationBuilder.AddColumn<string>(
                name: "telegram_login",
                table: "userLocations",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Ник телеграм");

            migrationBuilder.CreateIndex(
                name: "ix_user_locations_telegram_login",
                table: "userLocations",
                column: "telegram_login",
                unique: true);
        }
    }
}
