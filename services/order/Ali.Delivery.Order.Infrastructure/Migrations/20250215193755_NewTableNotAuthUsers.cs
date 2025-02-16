using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewTableNotAuthUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_last_name",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "user_first_name",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "user_birth_day",
                table: "users",
                newName: "birth_day");

            migrationBuilder.AddColumn<Guid>(
                name: "not_auth_receiver_id",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "not_auth_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true, comment: "Телефонный номер незарегистрированного пользователя"),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Имя незарегистрированного пользователя"),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Фамилия незарегистрированного пользователя"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_not_auth_users", x => x.id);
                },
                comment: "Незарегистрированный пользователь");

            migrationBuilder.CreateIndex(
                name: "ix_orders_not_auth_receiver_id",
                table: "orders",
                column: "not_auth_receiver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_not_auth_users_not_auth_receiver_id",
                table: "orders",
                column: "not_auth_receiver_id",
                principalTable: "not_auth_users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_not_auth_users_not_auth_receiver_id",
                table: "orders");

            migrationBuilder.DropTable(
                name: "not_auth_users");

            migrationBuilder.DropIndex(
                name: "ix_orders_not_auth_receiver_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "not_auth_receiver_id",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "users",
                newName: "user_last_name");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "users",
                newName: "user_first_name");

            migrationBuilder.RenameColumn(
                name: "birth_day",
                table: "users",
                newName: "user_birth_day");
        }
    }
}
