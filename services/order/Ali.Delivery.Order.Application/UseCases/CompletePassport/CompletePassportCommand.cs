using Ali.Delivery.Order.Application.Dtos.Enums;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CompletePassport;

/// <summary>
/// Представляет команду заполнения паспорта.
/// </summary>
/// <param name="PassportTypeCode">Тип паспорта.</param>
/// <param name="PassportNumber">Номер паспорта.</param>
/// <param name="RegDate">Дата регистрации паспорта.</param>
/// <param name="IssuedBy">Кем выдан.</param>
/// <param name="FirstName">Имя пользователя.</param>
/// <param name="LastName">Фамилия пользователя.</param>
public record CompletePassportCommand(PassportTypeCode PassportTypeCode, string PassportNumber, DateTime RegDate, string IssuedBy, string FirstName, string LastName) : IRequest<Guid>;
