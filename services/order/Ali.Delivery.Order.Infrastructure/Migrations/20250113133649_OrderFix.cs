using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "receiver_id",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_receiver_id",
                table: "orders",
                column: "receiver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_receiver_id",
                table: "orders",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_receiver_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_receiver_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "receiver_id",
                table: "orders");
        }
    }
}
