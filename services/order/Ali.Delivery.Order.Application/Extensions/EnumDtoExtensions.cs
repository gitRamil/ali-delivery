using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportType;
using OrderStatus = Ali.Delivery.Order.Application.Dtos.Order.OrderStatus;
using PassportType = Ali.Delivery.Order.Domain.Entities.Dictionaries.PassportType;
using RoleCode = Ali.Delivery.Order.Application.Dtos.Order.RoleCode;
using SizeCode = Ali.Delivery.Order.Application.Dtos.Order.SizeCode;

namespace Ali.Delivery.Order.Application.Extensions;

/// <summary>
/// Содержит набор методов расширения для работы с перечислениями.
/// </summary>
public static class EnumDtoExtensions
{
    #region OrderStatus
    /// <summary>
    /// Преобразует код статуса заказа в значение перечисления.
    /// </summary>
    public static OrderStatus ToOrderStatus(this OrderStatusCode orderStatusCode) =>
        orderStatusCode switch
        {
            not null when orderStatusCode == Domain.Entities.Dictionaries.OrderStatus.Created.Code => OrderStatus.Created,
            not null when orderStatusCode == Domain.Entities.Dictionaries.OrderStatus.Finished.Code => OrderStatus.Finished,
            _ => throw new ArgumentOutOfRangeException(nameof(orderStatusCode), orderStatusCode, "Не поддерживаемое значение статуса заказа.")
        };

    /// <summary>
    /// Преобразует значение перечисления в статус заказа.
    /// </summary>
    public static Domain.Entities.Dictionaries.OrderStatus ToOrderStatus(this OrderStatus codeEnum) =>
        codeEnum switch
        {
            OrderStatus.Created => Domain.Entities.Dictionaries.OrderStatus.Created,
            OrderStatus.Finished => Domain.Entities.Dictionaries.OrderStatus.Finished,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение статуса заказа.")
        };
    #endregion

    #region PassportType
    /// <summary>
    /// Преобразует код типа паспорта в значение перечисления.
    /// </summary>
    public static Dtos.Order.PassportType ToPassportType(this PassportTypeCode passportTypeCode) =>
        passportTypeCode switch
        {
            not null when passportTypeCode == PassportType.Diplomatic.Code => Dtos.Order.PassportType.Diplomatic,
            not null when passportTypeCode == PassportType.Internal.Code => Dtos.Order.PassportType.Internal,
            not null when passportTypeCode == PassportType.International.Code => Dtos.Order.PassportType.International,
            _ => throw new ArgumentOutOfRangeException(nameof(passportTypeCode), passportTypeCode, "Не поддерживаемое значение типа паспорта.")
        };

    /// <summary>
    /// Преобразует значение перечисления в тип паспорта.
    /// </summary>
    public static PassportType ToPassportType(this Dtos.Order.PassportType codeEnum) =>
        codeEnum switch
        {
            Dtos.Order.PassportType.Diplomatic => PassportType.Diplomatic,
            Dtos.Order.PassportType.Internal => PassportType.Internal,
            Dtos.Order.PassportType.International => PassportType.International,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение типа паспорта.")
        };
    #endregion

    #region RoleCode
    /// <summary>
    /// Преобразует код роли в значение перечисления.
    /// </summary>
    public static RoleCode ToRole(this Domain.ValueObjects.Dictionaries.Role.RoleCode roleCode) =>
        roleCode switch
        {
            not null when roleCode == Role.BasicUser.Code => RoleCode.BasicUser,
            not null when roleCode == Role.Courier.Code => RoleCode.Courier,
            not null when roleCode == Role.NotAuthUser.Code => RoleCode.NotAuthUser,
            _ => throw new ArgumentOutOfRangeException(nameof(roleCode), roleCode, "Не поддерживаемое значение роли.")
        };

    /// <summary>
    /// Преобразует значение перечисления в роль.
    /// </summary>
    public static Role ToRole(this RoleCode code) =>
        code switch
        {
            RoleCode.BasicUser => Role.BasicUser,
            RoleCode.Courier => Role.Courier,
            RoleCode.NotAuthUser => Role.NotAuthUser,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, "Не поддерживаемое значение роли.")
        };
    #endregion

    #region SizeCode
    /// <summary>
    /// Преобразует код размера в значение перечисления.
    /// </summary>
    public static SizeCode ToSize(this Domain.ValueObjects.Dictionaries.Size.SizeCode sizeCode) =>
        sizeCode switch
        {
            not null when sizeCode == Size.Small.Code => SizeCode.Small,
            not null when sizeCode == Size.Medium.Code => SizeCode.Medium,
            not null when sizeCode == Size.Large.Code => SizeCode.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(sizeCode), sizeCode, "Не поддерживаемое значение размера.")
        };

    /// <summary>
    /// Преобразует значение перечисления в размер.
    /// </summary>
    public static Size ToSize(this SizeCode code) =>
        code switch
        {
            SizeCode.Small => Size.Small,
            SizeCode.Medium => Size.Medium,
            SizeCode.Large => Size.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, "Не поддерживаемое значение размера.")
        };
    #endregion
}
