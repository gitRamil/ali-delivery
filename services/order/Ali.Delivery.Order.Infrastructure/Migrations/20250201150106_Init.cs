using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_statuses",
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
                    table.PrimaryKey("pk_order_statuses", x => x.id);
                },
                comment: "Справочник статусов заказов");

            migrationBuilder.CreateTable(
                name: "passport_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код типа паспорта"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Наименование типа паспорта"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_passport_types", x => x.id);
                },
                comment: "Справочник типов паспортов");

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<int>(type: "integer", nullable: false, comment: "Код"),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Наименование"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                },
                comment: "Справочник доступа пользователей");

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

            migrationBuilder.CreateTable(
                name: "sizes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Наименование"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sizes", x => x.id);
                },
                comment: "Справочник размеров");

            migrationBuilder.CreateTable(
                name: "passport",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата истечения срока действия паспорта"),
                    passport_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "Номер паспорта"),
                    registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата регистрации"),
                    type_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификационный номер типа паспорта"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_passport", x => x.id);
                    table.ForeignKey(
                        name: "fk_passport_passport_types_type_id",
                        column: x => x.type_id,
                        principalTable: "passport_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Информация о паспортах");

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор роли"),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор разрешения"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Отношение м:м ролей к разрешениям");

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    address_from = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Адрес отправления"),
                    address_to = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Адрес доставки"),
                    price = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена заказа"),
                    weight = table.Column<decimal>(type: "numeric", nullable: false, comment: "Вес заказа"),
                    size_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификационный номер размера"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_details_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Информация о заказе");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Логин пользователя"),
                    passport_info_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Информация о паспорте"),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Пароль пользователя"),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Роль пользователя"),
                    user_birth_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата рождения пользователя"),
                    user_first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Имя пользователя"),
                    user_last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Фамилия пользователя"),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_passport_passport_info_id",
                        column: x => x.passport_info_id,
                        principalTable: "passport",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    courier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Наименование заказа"),
                    details_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Информация о заказе"),
                    status_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Статус заказа"),
                    receiver_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_order_info_details_id",
                        column: x => x.details_id,
                        principalTable: "order_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_order_status_status_id",
                        column: x => x.status_id,
                        principalTable: "order_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_users_courier_id",
                        column: x => x.courier_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users_receiver_id",
                        column: x => x.receiver_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "users",
                        principalColumn: "id");
                },
                comment: "Заказ");

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
                table: "passport_types",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a15d9e1-c99f-95c5-162b-34f69121c4a1"), "diplomatic", null, "Дипломатический", null },
                    { new Guid("3a15d9e1-c9a0-80ab-eac9-9369b2ace783"), "internal", null, "Внутренний", null },
                    { new Guid("3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d"), "international", null, "Заграничный", null }
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
                table: "roles",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), "courier", null, "Курьер", null },
                    { new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), "basicUser", null, "Пользователь", null },
                    { new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"), "notAuthUser", null, "Неавторизованный пользователь", null }
                });

            migrationBuilder.InsertData(
                table: "sizes",
                columns: new[] { "id", "code", "created_by", "name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("3a156e1f-6055-abfb-36b7-7e630cc807b9"), "small", null, "Маленький", null },
                    { new Guid("3a156e1f-6056-875d-e42d-3e8e7ec6e082"), "medium", null, "Средний", null },
                    { new Guid("3a156e1f-6057-39cd-7580-20395231a00f"), "large", null, "Большой", null }
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
                name: "ix_order_details_size_id",
                table: "order_details",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_statuses_code",
                table: "order_statuses",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_courier_id",
                table: "orders",
                column: "courier_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_details_id",
                table: "orders",
                column: "details_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_receiver_id",
                table: "orders",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_sender_id",
                table: "orders",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_status_id",
                table: "orders",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_passport_type_id",
                table: "passport",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_passport_types_code",
                table: "passport_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_code",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_code",
                table: "roles",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sizes_code",
                table: "sizes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_passport_info_id",
                table: "users",
                column: "passport_info_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "sizes");

            migrationBuilder.DropTable(
                name: "passport");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "passport_types");
        }
    }
}
