using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("50ff84d1-016a-4a2b-ba53-51d6dd70459d"));

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("5ded1989-e506-448f-987d-7ad50574148b"));

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код"),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Наименование"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                },
                comment: "Справочник ролей пользователей");

            migrationBuilder.CreateIndex(
                name: "ix_roles_code",
                table: "roles",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("50ff84d1-016a-4a2b-ba53-51d6dd70459d"), "individual", null, "Индивидуальная", null },
                    { new Guid("5ded1989-e506-448f-987d-7ad50574148b"), "functional", null, "Функциональная", null }
                });
        }
    }
}
