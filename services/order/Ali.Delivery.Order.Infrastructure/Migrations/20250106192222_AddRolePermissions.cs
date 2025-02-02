using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_role_permissions",
                table: "role_permissions");

            migrationBuilder.DropIndex(
                name: "ix_role_permissions_role_id",
                table: "role_permissions");

            migrationBuilder.DropColumn(
                name: "token",
                table: "role_permissions");

            migrationBuilder.AlterTable(
                name: "role_permissions",
                comment: "Отношение м:м ролей к разрешениям");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "role_permissions",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор роли",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор роли")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "role_permissions",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор разрешения",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор разрешения")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_permissions",
                table: "role_permissions",
                columns: new[] { "role_id", "permission_id" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_role_permissions",
                table: "role_permissions");

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

            migrationBuilder.AlterTable(
                name: "role_permissions",
                oldComment: "Отношение м:м ролей к разрешениям");

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "role_permissions",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор разрешения",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор разрешения")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "role_permissions",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор роли",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор роли")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "role_permissions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                comment: "JWT токен, связанный с разрешением роли");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_permissions",
                table: "role_permissions",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_role_id",
                table: "role_permissions",
                column: "role_id");
        }
    }
}
