using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a15d9e1-c989-2e49-e8d3-55a56db7a2e1"));

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a15d9e1-c99e-6357-1416-7c7be54dd2a5"));

            migrationBuilder.AddColumn<Guid>(
                name: "courier_id",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a174d9d-c0df-65fa-4178-e9b514ce133d"), "finished", null, "Завершена", null },
                    { new Guid("3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a"), "created", null, "Создана", null },
                    { new Guid("3a174d9d-c0e1-358f-01db-927e1290e9f1"), "inProgress", null, "В процессе", null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_courier_id",
                table: "orders",
                column: "courier_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_courier_id",
                table: "orders",
                column: "courier_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_courier_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_courier_id",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a174d9d-c0df-65fa-4178-e9b514ce133d"));

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a"));

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a174d9d-c0e1-358f-01db-927e1290e9f1"));

            migrationBuilder.DropColumn(
                name: "courier_id",
                table: "orders");

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a15d9e1-c989-2e49-e8d3-55a56db7a2e1"), "created", null, "Создана", null },
                    { new Guid("3a15d9e1-c99e-6357-1416-7c7be54dd2a5"), "finished", null, "Завершена", null }
                });
        }
    }
}
