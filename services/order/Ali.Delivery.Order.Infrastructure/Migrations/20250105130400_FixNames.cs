using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_info_sizes_size_id",
                table: "order_info");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_order_info_order_info_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_order_status_order_status_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_passport_info_passport_types_passport_type_id",
                table: "passport_info");

            migrationBuilder.DropForeignKey(
                name: "fk_users_passport_info_passport_info_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_passport_info",
                table: "passport_info");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_info",
                table: "order_info");

            migrationBuilder.RenameTable(
                name: "passport_info",
                newName: "passport");

            migrationBuilder.RenameTable(
                name: "order_info",
                newName: "order_details");

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

            migrationBuilder.RenameColumn(
                name: "passport_info_id",
                table: "users",
                newName: "passport_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_passport_info_id",
                table: "users",
                newName: "ix_users_passport_id");

            migrationBuilder.RenameColumn(
                name: "order_status_id",
                table: "orders",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "order_info_id",
                table: "orders",
                newName: "details_id");

            migrationBuilder.RenameIndex(
                name: "ix_orders_order_status_id",
                table: "orders",
                newName: "ix_orders_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_orders_order_info_id",
                table: "orders",
                newName: "ix_orders_details_id");

            migrationBuilder.RenameColumn(
                name: "passport_info_reg_date",
                table: "passport",
                newName: "registration_date");

            migrationBuilder.RenameColumn(
                name: "passport_info_passport_number",
                table: "passport",
                newName: "passport_number");

            migrationBuilder.RenameColumn(
                name: "passport_info_expiration_date",
                table: "passport",
                newName: "expiration_date");

            migrationBuilder.RenameColumn(
                name: "passport_type_id",
                table: "passport",
                newName: "type_id");

            migrationBuilder.RenameIndex(
                name: "ix_passport_info_passport_type_id",
                table: "passport",
                newName: "ix_passport_type_id");

            migrationBuilder.RenameColumn(
                name: "order_info_weight",
                table: "order_details",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "order_info_price",
                table: "order_details",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "order_info_address_to",
                table: "order_details",
                newName: "address_to");

            migrationBuilder.RenameColumn(
                name: "order_info_address_from",
                table: "order_details",
                newName: "address_from");

            migrationBuilder.RenameIndex(
                name: "ix_order_info_size_id",
                table: "order_details",
                newName: "ix_order_details_size_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_passport",
                table: "passport",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_details",
                table: "order_details",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_sizes_size_id",
                table: "order_details",
                column: "size_id",
                principalTable: "sizes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_order_info_details_id",
                table: "orders",
                column: "details_id",
                principalTable: "order_details",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_order_status_status_id",
                table: "orders",
                column: "status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_passport_passport_types_type_id",
                table: "passport",
                column: "type_id",
                principalTable: "passport_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_passport_passport_id",
                table: "users",
                column: "passport_id",
                principalTable: "passport",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_sizes_size_id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_order_info_details_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_order_status_status_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_passport_passport_types_type_id",
                table: "passport");

            migrationBuilder.DropForeignKey(
                name: "fk_users_passport_passport_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_passport",
                table: "passport");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_details",
                table: "order_details");

            migrationBuilder.RenameTable(
                name: "passport",
                newName: "passport_info");

            migrationBuilder.RenameTable(
                name: "order_details",
                newName: "order_info");

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

            migrationBuilder.RenameColumn(
                name: "passport_id",
                table: "users",
                newName: "passport_info_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_passport_id",
                table: "users",
                newName: "ix_users_passport_info_id");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "orders",
                newName: "order_status_id");

            migrationBuilder.RenameColumn(
                name: "details_id",
                table: "orders",
                newName: "order_info_id");

            migrationBuilder.RenameIndex(
                name: "ix_orders_status_id",
                table: "orders",
                newName: "ix_orders_order_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_orders_details_id",
                table: "orders",
                newName: "ix_orders_order_info_id");

            migrationBuilder.RenameColumn(
                name: "registration_date",
                table: "passport_info",
                newName: "passport_info_reg_date");

            migrationBuilder.RenameColumn(
                name: "passport_number",
                table: "passport_info",
                newName: "passport_info_passport_number");

            migrationBuilder.RenameColumn(
                name: "expiration_date",
                table: "passport_info",
                newName: "passport_info_expiration_date");

            migrationBuilder.RenameColumn(
                name: "type_id",
                table: "passport_info",
                newName: "passport_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_passport_type_id",
                table: "passport_info",
                newName: "ix_passport_info_passport_type_id");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "order_info",
                newName: "order_info_weight");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "order_info",
                newName: "order_info_price");

            migrationBuilder.RenameColumn(
                name: "address_to",
                table: "order_info",
                newName: "order_info_address_to");

            migrationBuilder.RenameColumn(
                name: "address_from",
                table: "order_info",
                newName: "order_info_address_from");

            migrationBuilder.RenameIndex(
                name: "ix_order_details_size_id",
                table: "order_info",
                newName: "ix_order_info_size_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_passport_info",
                table: "passport_info",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_info",
                table: "order_info",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_info_sizes_size_id",
                table: "order_info",
                column: "size_id",
                principalTable: "sizes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_order_info_order_info_id",
                table: "orders",
                column: "order_info_id",
                principalTable: "order_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_order_status_order_status_id",
                table: "orders",
                column: "order_status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_passport_info_passport_types_passport_type_id",
                table: "passport_info",
                column: "passport_type_id",
                principalTable: "passport_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_passport_info_passport_info_id",
                table: "users",
                column: "passport_info_id",
                principalTable: "passport_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
