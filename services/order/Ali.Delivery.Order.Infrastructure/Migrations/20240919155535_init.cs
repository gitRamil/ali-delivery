using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    test = table.Column<bool>(type: "boolean", nullable: false, comment: "Test"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                },
                comment: "Заказ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
