using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewTableNotAuthUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notAuthUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Роль незарегистрированного пользователя"),
                    not_auth_phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true, comment: "Телефонный номер незарегистрированного пользователя"),
                    not_auth_user_first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Имя незарегистрированного пользователя"),
                    not_auth_user_last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Фамилия незарегистрированного пользователя"),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_not_auth_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_not_auth_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_not_auth_users_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id");
                },
                comment: "Незарегистрированный пользователь");

            migrationBuilder.CreateIndex(
                name: "ix_not_auth_users_creator_id",
                table: "notAuthUsers",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_not_auth_users_role_id",
                table: "notAuthUsers",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notAuthUsers");
        }
    }
}
