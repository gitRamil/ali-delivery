using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PassportFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_passport_passport_info_id",
                table: "users");

            migrationBuilder.AlterColumn<Guid>(
                name: "passport_info_id",
                table: "users",
                type: "uuid",
                nullable: true,
                comment: "Информация о паспорте",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Информация о паспорте");

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
                name: "fk_users_passport_passport_info_id",
                table: "users");

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
