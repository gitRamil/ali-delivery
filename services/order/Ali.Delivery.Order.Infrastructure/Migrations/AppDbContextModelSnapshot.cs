﻿// <auto-generated />
using System;
using Ali.Delivery.Order.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("OrderStatusId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_status_id")
                        .HasComment("Статус заказа");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.Property<Guid>("order_info_id")
                        .HasColumnType("uuid")
                        .HasColumnName("order_info_id")
                        .HasComment("Информация о заказе");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("OrderStatusId")
                        .HasDatabaseName("ix_orders_order_status_id");

                    b.HasIndex("order_info_id")
                        .HasDatabaseName("ix_orders_order_info_id");

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

                    b.Property<string>("AddressFrom")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("address_from")
                        .HasComment("Адрес отправления");

                    b.Property<string>("AddressTo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("address_to")
                        .HasComment("Адрес доставки");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price")
                        .HasComment("Цена заказа");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric")
                        .HasColumnName("weight")
                        .HasComment("Вес заказа");

                    b.Property<Guid>("size_id")
                        .HasColumnType("uuid")
                        .HasColumnName("size_id")
                        .HasComment("Идентификационный номер размера");

                    b.HasKey("Id")
                        .HasName("pk_order_info");

                    b.HasIndex("size_id")
                        .HasDatabaseName("ix_order_info_size_id");

                    b.ToTable("order_info", null, t =>
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

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_date")
                        .HasComment("Дата истечения срока действия паспорта");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("passport_number")
                        .HasComment("Номер паспорта");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reg_date")
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

                    b.Property<Guid>("passport_type_id")
                        .HasColumnType("uuid")
                        .HasColumnName("passport_type_id")
                        .HasComment("Идентификационный номер типа паспорта");

                    b.HasKey("Id")
                        .HasName("pk_passport_info");

                    b.HasIndex("passport_type_id")
                        .HasDatabaseName("ix_passport_info_passport_type_id");

                    b.ToTable("passport_info", null, t =>
                        {
                            t.HasComment("Информация о паспортах");
                        });
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday")
                        .HasComment("Дата рождения пользователя");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("created_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name")
                        .HasComment("Имя пользователя");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name")
                        .HasComment("Фамилия пользователя");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)))
                        .HasColumnName("updated_date");

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
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_order_status_order_status_id");

                    b.HasOne("Ali.Delivery.Order.Domain.Entities.OrderInfo", "OrderInfo")
                        .WithMany()
                        .HasForeignKey("order_info_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_order_info_order_info_id");

                    b.Navigation("OrderInfo");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.OrderInfo", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.Size", "Size")
                        .WithMany()
                        .HasForeignKey("size_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_info_sizes_size_id");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.PassportInfo", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.Dictionaries.PassportType", "PassportType")
                        .WithMany()
                        .HasForeignKey("passport_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_passport_info_passport_types_passport_type_id");

                    b.Navigation("PassportType");
                });

            modelBuilder.Entity("Ali.Delivery.Order.Domain.Entities.User", b =>
                {
                    b.HasOne("Ali.Delivery.Order.Domain.Entities.PassportInfo", "PassportInfo")
                        .WithMany()
                        .HasForeignKey("passport_info_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_passport_info_passport_info_id");

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
