﻿// <auto-generated />
using System;
using Ali.Delivery.Order.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250106192222_AddRolePermissions")]
    partial class AddRolePermissions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("code")
                        .HasComment("Код");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name")
                        .HasComment("Наименование");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_order_statuses");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_order_statuses_code");

                    b.ToTable("order_statuses", null, t =>
                        {
                            t.HasComment("Справочник статусов заказов");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a15d9e1-c989-2e49-e8d3-55a56db7a2e1"),
                            Code = "created",
                            Name = "Создана"
                        },
                        new
                        {
                            Id = new Guid("3a15d9e1-c99e-6357-1416-7c7be54dd2a5"),
                            Code = "finished",
                            Name = "Завершена"
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Dictionaries.PassportType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("code")
                        .HasComment("Код типа паспорта");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name")
                        .HasComment("Наименование типа паспорта");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_passport_types");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_passport_types_code");

                    b.ToTable("passport_types", null, t =>
                        {
                            t.HasComment("Справочник типов паспортов");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a15d9e1-c9a0-80ab-eac9-9369b2ace783"),
                            Code = "internal",
                            Name = "Внутренний"
                        },
                        new
                        {
                            Id = new Guid("3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d"),
                            Code = "international",
                            Name = "Заграничный"
                        },
                        new
                        {
                            Id = new Guid("3a15d9e1-c99f-95c5-162b-34f69121c4a1"),
                            Code = "diplomatic",
                            Name = "Дипломатический"
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Dictionaries.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<int>("Code")
                        .HasColumnType("integer")
                        .HasColumnName("code")
                        .HasComment("Код");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name")
                        .HasComment("Наименование");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_permissions");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_permissions_code");

                    b.ToTable("permissions", null, t =>
                        {
                            t.HasComment("Справочник доступа пользователей");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"),
                            Code = 1000,
                            Name = "Доступ создания пользователя"
                        },
                        new
                        {
                            Id = new Guid("3a166cc9-7999-9fc9-2798-85b0ef75288d"),
                            Code = 1003,
                            Name = "Доступ обновления заказа"
                        },
                        new
                        {
                            Id = new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"),
                            Code = 1002,
                            Name = "Доступ просмотра заказа"
                        },
                        new
                        {
                            Id = new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"),
                            Code = 1001,
                            Name = "Доступ удаления заказа"
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Dictionaries.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("code")
                        .HasComment("Код");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name")
                        .HasComment("Наименование");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_roles_code");

                    b.ToTable("roles", null, t =>
                        {
                            t.HasComment("Справочник ролей пользователей");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"),
                            Code = "courier",
                            Name = "Курьер"
                        },
                        new
                        {
                            Id = new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"),
                            Code = "basicUser",
                            Name = "Пользователь"
                        },
                        new
                        {
                            Id = new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"),
                            Code = "notAuthUser",
                            Name = "Неавторизованный пользователь"
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Dictionaries.Size", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("code")
                        .HasComment("Код");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name")
                        .HasComment("Наименование");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_sizes");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_sizes_code");

                    b.ToTable("sizes", null, t =>
                        {
                            t.HasComment("Справочник размеров");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a156e1f-6055-abfb-36b7-7e630cc807b9"),
                            Code = "small",
                            Name = "Маленький"
                        },
                        new
                        {
                            Id = new Guid("3a156e1f-6056-875d-e42d-3e8e7ec6e082"),
                            Code = "medium",
                            Name = "Средний"
                        },
                        new
                        {
                            Id = new Guid("3a156e1f-6057-39cd-7580-20395231a00f"),
                            Code = "large",
                            Name = "Большой"
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name")
                        .HasComment("Наименование заказа");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.Property<Guid>("details_id")
                        .HasColumnType("uuid")
                        .HasColumnName("details_id")
                        .HasComment("Информация о заказе");

                    b.Property<Guid?>("sender_id")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_id");

                    b.Property<Guid>("status_id")
                        .HasColumnType("uuid")
                        .HasColumnName("status_id")
                        .HasComment("Статус заказа");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("details_id")
                        .HasDatabaseName("ix_orders_details_id");

                    b.HasIndex("sender_id")
                        .HasDatabaseName("ix_orders_sender_id");

                    b.HasIndex("status_id")
                        .HasDatabaseName("ix_orders_status_id");

                    b.ToTable("orders", null, t =>
                        {
                            t.HasComment("Заказ");
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.OrderInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("OrderInfoAddressFrom")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("address_from")
                        .HasComment("Адрес отправления");

                    b.Property<string>("OrderInfoAddressTo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("address_to")
                        .HasComment("Адрес доставки");

                    b.Property<decimal>("OrderInfoPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("price")
                        .HasComment("Цена заказа");

                    b.Property<decimal>("OrderInfoWeight")
                        .HasColumnType("numeric")
                        .HasColumnName("weight")
                        .HasComment("Вес заказа");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.Property<Guid>("size_id")
                        .HasColumnType("uuid")
                        .HasColumnName("size_id")
                        .HasComment("Идентификационный номер размера");

                    b.HasKey("Id")
                        .HasName("pk_order_details");

                    b.HasIndex("size_id")
                        .HasDatabaseName("ix_order_details_size_id");

                    b.ToTable("order_details", null, t =>
                        {
                            t.HasComment("Информация о заказе");
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.PassportInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<DateTime>("PassportInfoExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_date")
                        .HasComment("Дата истечения срока действия паспорта");

                    b.Property<string>("PassportInfoPassportNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("passport_number")
                        .HasComment("Номер паспорта");

                    b.Property<DateTime>("PassportInfoRegDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("registration_date")
                        .HasComment("Дата регистрации");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.Property<Guid>("type_id")
                        .HasColumnType("uuid")
                        .HasColumnName("type_id")
                        .HasComment("Идентификационный номер типа паспорта");

                    b.HasKey("Id")
                        .HasName("pk_passport");

                    b.HasIndex("type_id")
                        .HasDatabaseName("ix_passport_type_id");

                    b.ToTable("passport", null, t =>
                        {
                            t.HasComment("Информация о паспортах");
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id")
                        .HasColumnOrder(1)
                        .HasComment("Идентификатор роли");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid")
                        .HasColumnName("permission_id")
                        .HasColumnOrder(2)
                        .HasComment("Идентификатор разрешения");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("pk_role_permissions");

                    b.HasIndex("PermissionId")
                        .HasDatabaseName("ix_role_permissions_permission_id");

                    b.ToTable("role_permissions", null, t =>
                        {
                            t.HasComment("Отношение м:м ролей к разрешениям");
                        });

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"),
                            PermissionId = new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"),
                            Id = new Guid("3a174d9d-c0dd-8c7a-dd0b-1aa0c32ce21a")
                        },
                        new
                        {
                            RoleId = new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"),
                            PermissionId = new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"),
                            Id = new Guid("3a174d9d-c0db-4d94-ec59-c6cf4e208981")
                        },
                        new
                        {
                            RoleId = new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"),
                            PermissionId = new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"),
                            Id = new Guid("3a174d9d-c0d6-4039-8fc1-bd00fe7d8724")
                        },
                        new
                        {
                            RoleId = new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"),
                            PermissionId = new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"),
                            Id = new Guid("3a174d9d-c0d9-1ae6-97f3-4cf384519fe5")
                        },
                        new
                        {
                            RoleId = new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"),
                            PermissionId = new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"),
                            Id = new Guid("3a174d9d-c0da-816f-c8bc-d870eec16d5b")
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("login")
                        .HasComment("Логин пользователя");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("password")
                        .HasComment("Пароль пользователя");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.Property<DateTime>("UserBirthDay")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("user_birth_day")
                        .HasComment("Дата рождения пользователя");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("user_first_name")
                        .HasComment("Имя пользователя");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("user_last_name")
                        .HasComment("Фамилия пользователя");

                    b.Property<Guid>("passport_info_id")
                        .HasColumnType("uuid")
                        .HasColumnName("passport_info_id")
                        .HasComment("Информация о паспорте");

                    b.Property<Guid>("role_id")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id")
                        .HasComment("Роль пользователя");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("passport_info_id")
                        .HasDatabaseName("ix_users_passport_info_id");

                    b.HasIndex("role_id")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", null, t =>
                        {
                            t.HasComment("Пользователь");
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.Order", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.OrderInfo", "OrderInfo")
                        .WithMany()
                        .HasForeignKey("details_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_order_info_details_id");

                    b.HasOne("Ali.Delivery.Order.Domain.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("sender_id")
                        .HasConstraintName("fk_orders_users_sender_id");

                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_order_status_status_id");

                    b.Navigation("OrderInfo");

                    b.Navigation("OrderStatus");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.OrderInfo", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.Size", "Size")
                        .WithMany()
                        .HasForeignKey("size_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_details_sizes_size_id");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.PassportInfo", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.PassportType", "PassportType")
                        .WithMany()
                        .HasForeignKey("type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_passport_passport_types_type_id");

                    b.Navigation("PassportType");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permissions_permissions_permission_id");

                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permissions_roles_role_id");

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.User", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.PassportInfo", "PassportInfo")
                        .WithMany()
                        .HasForeignKey("passport_info_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_passport_passport_info_id");

                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.Role", "Role")
                        .WithMany()
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_roles_role_id");

                    b.Navigation("PassportInfo");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
