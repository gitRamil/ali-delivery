using Ali.Delivery.Order.Domain.ValueObjects.User;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация сущности User для базы данных.
    /// </summary>
    internal class UserConfiguration : EntityTypeConfigurationBase<Domain.Entities.User>
    {
        /// <summary>
        /// Конфигурирует сущность User.
        /// </summary>
        /// <param name="builder">Построитель конфигурации Entity Framework.</param>
        protected override void OnConfigure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            // Указываем таблицу для сущности User
            builder.ToTable("Users", t => t.HasComment("Пользователи"));
            
            // Настраиваем поле FirstName (Имя пользователя)
            builder.Property(u => u.FirstName)
                   .HasComment("Имя пользователя")
                   .IsRequired()
                   .HasMaxLength(FirstName.MaxLength)
                   .HasConversion(v => v.ToString(), v => new FirstName(v));

            // Настраиваем поле LastName (Фамилия пользователя)
            builder.Property(u => u.LastName)
                   .HasConversion(v => v.ToString(), v => new LastName(v))
                   .HasComment("Фамилия пользователя")
                   .IsRequired()
                   .HasMaxLength(LastName.MaxLength);

            // Настраиваем поле PassportInfo (Паспортные данные пользователя)
            // builder.Property(u => u.PassportInfo)
                   

            // Устанавливаем связь с таблицей PassportInfo
            // builder.HasOne(u => u.PassportInfo)  
                  

            // Настраиваем поле Email (Электронная почта пользователя)
            builder.Property(u => u.Email)
                   .HasConversion(v => v.ToString(), v => new UserEmail(v))
                   .HasColumnName("Email")
                   .IsRequired()
                   .HasMaxLength(100);

            // Настраиваем поле PhoneNumber (Номер телефона пользователя)
            builder.Property(u => u.PhoneNumber)
                   .HasConversion(v => v.ToString(), v => new PhoneNumber(v))
                   .HasComment("PhoneNumber")
                   .IsRequired()
                   .HasMaxLength(20);

            // Настраиваем поле RoleId (Идентификатор роли пользователя)
            builder.HasOne(u => u.RoleId)
                   .WithMany()
                   .HasForeignKey("RoleId");

            builder.Property("RoleId")
                   .HasComment("Идентификатор ролей пользователей");

            // Настраиваем поле BirthDay (Дата рождения пользователя)
            builder.Property(u => u.BirthDay)
                   .HasComment("Дата рождения")
                   .IsRequired();

            // Добавляем уникальные индексы на Email для ускорения поиска и обеспечения уникальности
            builder.HasIndex(u => u.Email)
                   .IsUnique(); 

            // Добавляем уникальные индексы на PhoneNumber для ускорения поиска и обеспечения уникальности
            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique(); 
        }
    }
}
