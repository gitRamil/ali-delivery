using System.Runtime.Serialization;

namespace Ali.Delivery.Order.Application.Exceptions;

/// <summary>
/// Представляет исключение, возникающее в случае, когда запрашиваемая сущность не найдена.
/// </summary>
[Serializable]
public sealed class NotFoundException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotFoundException" />.
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotFoundException" />.
    /// </summary>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    public NotFoundException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public NotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotFoundException" />.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <param name="key">Ключ.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="type" /> или
    /// <paramref name="key" /> равен <c>null</c>.
    /// </exception>
    public NotFoundException(Type type, object key)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        Details = $"Сущность {type.FullName} с ключем {key} не найдена.";
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotFoundException" />.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <param name="key">Ключ.</param>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="type" /> или <paramref name="key" /> равен <c>null</c>.
    /// </exception>
    public NotFoundException(Type type, object key, string? message)
        : base(message)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(key);

        Details = $"Сущность {type.FullName} с ключом {key} не найдена.";
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotFoundException" />.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <param name="key">Ключ.</param>
    /// <param name="message">Сообщение с описанием ошибки.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="type" /> или <paramref name="key" /> равен <c>null</c>.
    /// </exception>
    public NotFoundException(Type type, object key, string? message, Exception? innerException)
        : base(message, innerException)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(key);

        Details = $"Сущность {type.FullName} с ключом {key} не найдена.";
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationException" />.
    /// </summary>
    /// <param name="info">Информация для сериализации.</param>
    /// <param name="context">Контекст.</param>
    [Obsolete("Obsolete")]
    private NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context) =>
        Details = info.GetString(nameof(Details));

    /// <summary>
    /// Возвращает сведения об ошибке, содержащие тип сущности и ее ключ.
    /// </summary>
    public string? Details { get; }

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
        info.AddValue(nameof(Details), Details);
    }
}
