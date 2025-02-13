using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiration_date",
                table: "passport");

            migrationBuilder.AlterColumn<string>(
                name: "user_last_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "Фамилия пользователя",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldComment: "Фамилия пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "user_first_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "Имя пользователя",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldComment: "Имя пользователя");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "passport",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор типа паспорта",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификационный номер типа паспорта");

            migrationBuilder.AddColumn<string>(
                name: "issued_by",
                table: "passport",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "Кем выдан");

            migrationBuilder.AlterColumn<Guid>(
                name: "size_id",
                table: "order_details",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор размера",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификационный номер размера");

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_login",
                table: "users");

            migrationBuilder.DropColumn(
                name: "issued_by",
                table: "passport");

            migrationBuilder.AlterColumn<string>(
                name: "user_last_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Фамилия пользователя",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Фамилия пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "user_first_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Имя пользователя",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Имя пользователя");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "passport",
                type: "uuid",
                nullable: false,
                comment: "Идентификационный номер типа паспорта",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор типа паспорта");

            migrationBuilder.AddColumn<DateTime>(
                name: "expiration_date",
                table: "passport",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Дата истечения срока действия паспорта");

            migrationBuilder.AlterColumn<Guid>(
                name: "size_id",
                table: "order_details",
                type: "uuid",
                nullable: false,
                comment: "Идентификационный номер размера",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор размера");
        }
    }
}
