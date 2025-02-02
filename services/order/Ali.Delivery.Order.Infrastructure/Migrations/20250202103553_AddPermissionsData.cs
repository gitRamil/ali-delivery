using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_passport_passport_info_id",
                table: "users");

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a15d9e1-c989-2e49-e8d3-55a56db7a2e1"));

            migrationBuilder.DeleteData(
                table: "order_statuses",
                keyColumn: "id",
                keyValue: new Guid("3a15d9e1-c99e-6357-1416-7c7be54dd2a5"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a166cc9-7999-9fc9-2798-85b0ef75288d"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1") });

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"), new Guid("3a1537c0-11f8-d788-90d9-ced196c63397") });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"));

            migrationBuilder.AlterColumn<Guid>(
                name: "passport_info_id",
                table: "users",
                type: "uuid",
                nullable: true,
                comment: "Информация о паспорте",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Информация о паспорте");

            migrationBuilder.AddColumn<Guid>(
                name: "courier_id",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "receiver_id",
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

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a17be54-4e65-5dca-866e-f7cd3b8c49bb"), 1003, null, "Полный доступ", null },
                    { new Guid("3a17be54-4e80-5c77-723b-dc0991c878da"), 1001, null, "Доступ для работы с заказами", null },
                    { new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), 1002, null, "Доступ отслеживания заказов", null },
                    { new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"), 1004, null, "Доступ пользователя к работе с заказами", null },
                    { new Guid("3a17be54-4e83-e173-197a-4a19535ed222"), 1005, null, "Доступ курьера к работе с заказами", null },
                    { new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"), 1000, null, "Доступ для работы с сущностью пользователя", null }
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

            migrationBuilder.CreateIndex(
                name: "ix_orders_courier_id",
                table: "orders",
                column: "courier_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_receiver_id",
                table: "orders",
                column: "receiver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_courier_id",
                table: "orders",
                column: "courier_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_receiver_id",
                table: "orders",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_passport_passport_info_id",
                table: "users",
                column: "passport_info_id",
                principalTable: "passport",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_courier_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_receiver_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_users_passport_passport_info_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_orders_courier_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_receiver_id",
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

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a17be54-4e65-5dca-866e-f7cd3b8c49bb"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a17be54-4e80-5c77-723b-dc0991c878da"));

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
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a17be54-4e83-e173-197a-4a19535ed222"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"));

            migrationBuilder.DropColumn(
                name: "courier_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "receiver_id",
                table: "orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "passport_info_id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Информация о паспорте",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "Информация о паспорте");

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a15d9e1-c989-2e49-e8d3-55a56db7a2e1"), "created", null, "Создана", null },
                    { new Guid("3a15d9e1-c99e-6357-1416-7c7be54dd2a5"), "finished", null, "Завершена", null }
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a166cc9-7999-9fc9-2798-85b0ef75288d"), 1003, null, "Доступ обновления заказа", null },
                    { new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"), 1001, null, "Доступ удаления заказа", null },
                    { new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"), 1000, null, "Доступ создания пользователя", null },
                    { new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"), 1002, null, "Доступ просмотра заказа", null }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id", "created_by", "id", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), null, new Guid("3a174d9d-c0d9-1ae6-97f3-4cf384519fe5"), null },
                    { new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"), new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), null, new Guid("3a174d9d-c0da-816f-c8bc-d870eec16d5b"), null },
                    { new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), null, new Guid("3a174d9d-c0d6-4039-8fc1-bd00fe7d8724"), null },
                    { new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"), new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), null, new Guid("3a174d9d-c0db-4d94-ec59-c6cf4e208981"), null },
                    { new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"), new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"), null, new Guid("3a174d9d-c0dd-8c7a-dd0b-1aa0c32ce21a"), null }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_users_passport_passport_info_id",
                table: "users",
                column: "passport_info_id",
                principalTable: "passport",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
