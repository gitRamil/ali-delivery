using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Exceptions;

/// <summary>
/// Представляет исключение, возникающее в случае обнаружения ошибок достоверности.
/// </summary>
[Serializable]
public sealed class ValidationException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    public ValidationException() => Errors = new Dictionary<string, string[]>();

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    public ValidationException(string? message)
        : base(message) =>
        Errors = new Dictionary<string, string[]>();

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public ValidationException(string? message, Exception? innerException)
        : base(message, innerException) =>
        Errors = new Dictionary<string, string[]>();

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="errors">Словарь, содержащий описание ошибок.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="errors" /> равен <c>null</c>.
    /// </exception>
    public ValidationException(IDictionary<string, string[]> errors) => Errors = errors ?? throw new ArgumentNullException(nameof(errors));

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="errors">Словарь, содержащий описание ошибок по свойствам, не прошедшим проверку.</param>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="errors" /> равен <c>null</c>.
    /// </exception>
    public ValidationException(IDictionary<string, string[]> errors, string? message)
        : base(message) =>
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="errors">Словарь, содержащий описание ошибок по свойствам, не прошедшим проверку.</param>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="errors" /> равен <c>null</c>.
    /// </exception>
    public ValidationException(IDictionary<string, string[]> errors, string? message, Exception? innerException)
        : base(message, innerException) =>
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="info">Информация для сериализации.</param>
    /// <param name="context">Контекст.</param>
    [Obsolete("Obsolete")]
    private ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context) =>
        Errors = (IDictionary<string, string[]>)info.GetValue(nameof(Errors), typeof(IDictionary<string, string[]>))!;

    /// <summary>
    /// Возвращает словарь, содержащий описание ошибок по свойствам, не прошедшим проверку.
    /// </summary>
    /// <value>
    /// Словарь, содержащий описание ошибок по свойствам, не прошедшим проверку.
    /// </value>
    public IDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" />
    /// with information about the exception.
    /// </summary>
    /// <param name="info">
    /// The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
    /// data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
    /// information about the source or destination.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The <paramref name="info" /> parameter is a null reference (
    /// <see langword="Nothing" /> in Visual Basic).
    /// </exception>
    [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.",
              DiagnosticId = "SYSLIB0051",
              UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Errors), Errors);
    }
}
