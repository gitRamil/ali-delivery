using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotAuthUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_order_status_status_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_sender_id",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e83-e173-197a-4a19535ed222"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1537c0-11f8-d788-90d9-ced196c63397") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"));

            migrationBuilder.AlterTable(
                name: "users",
                comment: "Пользователи",
                oldComment: "Пользователь");

            migrationBuilder.AlterTable(
                name: "orders",
                comment: "Заказы",
                oldComment: "Заказ");

            migrationBuilder.AlterTable(
                name: "order_details",
                comment: "Информация о заказах",
                oldComment: "Информация о заказе");

            migrationBuilder.AlterTable(
                name: "not_auth_users",
                comment: "Незарегистрированные пользователи",
                oldComment: "Незарегистрированный пользователь");

            migrationBuilder.AlterColumn<Guid>(
                name: "sender_id",
                table: "orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "creator_user_id",
                table: "not_auth_users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Идентификатор пользователя, создавшего неавторизованного пользователя");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873"), "basicUser", null, "Пользователь", null },
                    { new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c"), "courier", null, "Курьер", null },
                    { new Guid("3a1844b2-1e61-d7b0-a9f6-8ec08e2c69fd"), "notAuthUser", null, "Неавторизованный пользователь", null },
                    { new Guid("3a1844b2-1e63-f523-2f04-f4eb3bad17b3"), "superUser", null, "Пользователь с полным доступом", null }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id", "created_by", "id", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873"), null, new Guid("3a1844e7-40b0-0bdb-5ad3-309f135ff5e2"), null },
                    { new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"), new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873"), null, new Guid("3a1844e7-409f-dd6f-39ed-1f2f67aef722"), null },
                    { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873"), null, new Guid("3a1844e7-40b1-d958-4573-cc84183ec531"), null },
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c"), null, new Guid("3a1844e7-40b4-6c99-9ee8-c6ed4958ad43"), null },
                    { new Guid("3a17be54-4e83-e173-197a-4a19535ed222"), new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c"), null, new Guid("3a1844e7-40b2-9ae9-56c9-27598b808a43"), null },
                    { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c"), null, new Guid("3a1844e7-40b3-790d-6f17-ffa82729df95"), null },
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1844b2-1e61-d7b0-a9f6-8ec08e2c69fd"), null, new Guid("3a1844e7-40b5-634c-f5e0-662dfe13dff1"), null },
                    { new Guid("3a17be54-4e65-5dca-866e-f7cd3b8c49bb"), new Guid("3a1844b2-1e63-f523-2f04-f4eb3bad17b3"), null, new Guid("3a1844e7-40b6-8c81-44cf-f1ff77037740"), null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_not_auth_users_creator_user_id",
                table: "not_auth_users",
                column: "creator_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_not_auth_users_users_creator_user_id",
                table: "not_auth_users",
                column: "creator_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_order_statuses_status_id",
                table: "orders",
                column: "status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_sender_id",
                table: "orders",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_not_auth_users_users_creator_user_id",
                table: "not_auth_users");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_order_statuses_status_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_sender_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_not_auth_users_creator_user_id",
                table: "not_auth_users");

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"), new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e83-e173-197a-4a19535ed222"), new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1844b2-1e61-d7b0-a9f6-8ec08e2c69fd") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a17be54-4e65-5dca-866e-f7cd3b8c49bb"), new Guid("3a1844b2-1e63-f523-2f04-f4eb3bad17b3") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1844b2-1e61-d7b0-a9f6-8ec08e2c69fd"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3a1844b2-1e63-f523-2f04-f4eb3bad17b3"));

            migrationBuilder.DropColumn(
                name: "creator_user_id",
                table: "not_auth_users");

            migrationBuilder.AlterTable(
                name: "users",
                comment: "Пользователь",
                oldComment: "Пользователи");

            migrationBuilder.AlterTable(
                name: "orders",
                comment: "Заказ",
                oldComment: "Заказы");

            migrationBuilder.AlterTable(
                name: "order_details",
                comment: "Информация о заказе",
                oldComment: "Информация о заказах");

            migrationBuilder.AlterTable(
                name: "not_auth_users",
                comment: "Незарегистрированный пользователь",
                oldComment: "Незарегистрированные пользователи");

            migrationBuilder.AlterColumn<Guid>(
                name: "sender_id",
                table: "orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), "courier", null, "Курьер", null },
                    { new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), "basicUser", null, "Пользователь", null },
                    { new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"), "notAuthUser", null, "Неавторизованный пользователь", null }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id", "created_by", "id", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), null, new Guid("3a17be5f-7bc2-9c5b-8a73-bcd6ce135188"), null },
                    { new Guid("3a17be54-4e83-e173-197a-4a19535ed222"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), null, new Guid("3a17be5f-7bc0-509f-e1ec-2f10d0cebef0"), null },
                    { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), null, new Guid("3a17be5f-7bc1-6952-64c0-1c9ca8881fd0"), null },
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), null, new Guid("3a17be5f-7bbe-2559-4048-48a0196e0f25"), null },
                    { new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), null, new Guid("3a17be5f-7b9c-0bc4-a94f-b0bba09fd370"), null },
                    { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), null, new Guid("3a17be5f-7bbf-849a-7d45-02bd0f50536d"), null },
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"), null, new Guid("3a17be5f-7bc6-ba8e-0f5d-b7192c99492b"), null }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_orders_order_status_status_id",
                table: "orders",
                column: "status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_sender_id",
                table: "orders",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
