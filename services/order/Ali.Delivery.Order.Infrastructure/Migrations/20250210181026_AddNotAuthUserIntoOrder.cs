using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotAuthUserIntoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "not_auth_receiver_id",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_not_auth_receiver_id",
                table: "orders",
                column: "not_auth_receiver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_not_auth_users_not_auth_receiver_id",
                table: "orders",
                column: "not_auth_receiver_id",
                principalTable: "notAuthUsers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_not_auth_users_not_auth_receiver_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_not_auth_receiver_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "not_auth_receiver_id",
                table: "orders");
        }
    }
}
