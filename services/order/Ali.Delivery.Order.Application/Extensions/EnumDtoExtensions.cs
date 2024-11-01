using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Role;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Size;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportType;

namespace Ali.Delivery.Order.Application.Extensions;

/// <summary>
/// Содержит набор методов расширения для работы с перечислениями.
/// </summary>
public static class EnumDtoExtensions
{
    // *** OrderStatus Extensions ***
    /// <summary>
    /// Преобразует код статуса заказа в значение перечисления.
    /// </summary>
    public static OrderStatusCodeEnum ToOrderStatus(this OrderStatusCode orderStatusCode) =>
        orderStatusCode switch
        {
            not null when orderStatusCode == OrderStatus.Created.Code => OrderStatusCodeEnum.Created,
            not null when orderStatusCode == OrderStatus.Finished.Code => OrderStatusCodeEnum.Finished,
            _ => throw new ArgumentOutOfRangeException(nameof(orderStatusCode), orderStatusCode, "Не поддерживаемое значение статуса заказа.")
        };

    /// <summary>
    /// Преобразует значение перечисления в статус заказа.
    /// </summary>
    public static OrderStatus ToOrderStatus(this OrderStatusCodeEnum codeEnum) =>
        codeEnum switch
        {
            OrderStatusCodeEnum.Created => OrderStatus.Created,
            OrderStatusCodeEnum.Finished => OrderStatus.Finished,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение статуса заказа.")
        };


    // *** Role Extensions ***
    /// <summary>
    /// Преобразует код роли в значение перечисления.
    /// </summary>
    public static RoleCodeEnum ToRole(this RoleCode roleCode) =>
        roleCode switch
        {
            not null when roleCode == Role.BasicUser.Code => RoleCodeEnum.BasicUser,
            not null when roleCode == Role.Courier.Code => RoleCodeEnum.Courier,
            not null when roleCode == Role.NotAuthUser.Code => RoleCodeEnum.NotAuthUser,
            _ => throw new ArgumentOutOfRangeException(nameof(roleCode), roleCode, "Не поддерживаемое значение роли.")
        };

    /// <summary>
    /// Преобразует значение перечисления в роль.
    /// </summary>
    public static Role ToRole(this RoleCodeEnum codeEnum) =>
        codeEnum switch
        {
            RoleCodeEnum.BasicUser => Role.BasicUser,
            RoleCodeEnum.Courier => Role.Courier,
            RoleCodeEnum.NotAuthUser => Role.NotAuthUser,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение роли.")
        };


    // *** PassportType Extensions ***
    /// <summary>
    /// Преобразует код типа паспорта в значение перечисления.
    /// </summary>
    public static PassportTypeCodeEnum ToPassportType(this PassportTypeCode passportTypeCode) =>
        passportTypeCode switch
        {
            not null when passportTypeCode == PassportType.Diplomatic.Code => PassportTypeCodeEnum.Diplomatic,
            not null when passportTypeCode == PassportType.Internal.Code => PassportTypeCodeEnum.Internal,
            not null when passportTypeCode == PassportType.International.Code => PassportTypeCodeEnum.International,
            _ => throw new ArgumentOutOfRangeException(nameof(passportTypeCode), passportTypeCode, "Не поддерживаемое значение типа паспорта.")
        };

    /// <summary>
    /// Преобразует значение перечисления в тип паспорта.
    /// </summary>
    public static PassportType ToPassportType(this PassportTypeCodeEnum codeEnum) =>
        codeEnum switch
        {
            PassportTypeCodeEnum.Diplomatic => PassportType.Diplomatic,
            PassportTypeCodeEnum.Internal => PassportType.Internal,
            PassportTypeCodeEnum.International => PassportType.International,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение типа паспорта.")
        };


    // *** Size Extensions ***
    /// <summary>
    /// Преобразует код размера в значение перечисления.
    /// </summary>
    public static SizeCodeEnum ToSize(this SizeCode sizeCode) =>
        sizeCode switch
        {
            not null when sizeCode == Size.Small.Code => SizeCodeEnum.Small,
            not null when sizeCode == Size.Medium.Code => SizeCodeEnum.Medium,
            not null when sizeCode == Size.Large.Code => SizeCodeEnum.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(sizeCode), sizeCode, "Не поддерживаемое значение размера.")
        };

    /// <summary>
    /// Преобразует значение перечисления в размер.
    /// </summary>
    public static Size ToSize(this SizeCodeEnum codeEnum) =>
        codeEnum switch
        {
            SizeCodeEnum.Small => Size.Small,
            SizeCodeEnum.Medium => Size.Medium,
            SizeCodeEnum.Large => Size.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение размера.")
        };
}
