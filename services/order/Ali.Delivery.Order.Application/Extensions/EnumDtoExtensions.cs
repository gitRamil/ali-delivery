using Ali.Delivery.Order.Domain.Entities.Dictionaries;
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
