using FluentValidation;

namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Описывает службу валидации.
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Производит валидацию объекта с помощью зарегистрированных <see cref="IValidator{T}" />.
    /// </summary>
    /// <param name="obj">Объект для валидации.</param>
    /// <exception cref="Exceptions.ValidationException">
    /// Возникает, если произошла ошибка валидации.
    /// </exception>
    void Validate<T>(T obj);

    /// <summary>
    /// Производит валидацию объекта с помощью зарегистрированных <see cref="IValidator{T}" />.
    /// </summary>
    /// <param name="obj">Объект для валидации.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="Exceptions.ValidationException">
    /// Возникает, если произошла ошибка валидации.
    /// </exception>
    Task ValidateAsync<T>(T obj, CancellationToken cancellationToken);
}
