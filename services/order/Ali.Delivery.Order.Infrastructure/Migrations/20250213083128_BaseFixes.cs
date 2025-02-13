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
            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "passport",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор типа паспорта",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификационный номер типа паспорта");

            migrationBuilder.AlterColumn<Guid>(
                name: "size_id",
                table: "order_details",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор размера",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификационный номер размера");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "passport",
                type: "uuid",
                nullable: false,
                comment: "Идентификационный номер типа паспорта",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор типа паспорта");

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
