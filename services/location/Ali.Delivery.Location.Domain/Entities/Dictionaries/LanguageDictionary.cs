using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;

namespace Ali.Delivery.Location.Domain.Entities.Dictionaries;

/// <summary>
/// Представляет словарь языков.
/// </summary>
public class LanguageDictionary : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает язык: English.
    /// </summary>
    public static readonly LanguageDictionary English = new(new Guid("3a156e1f-6091-875d-e42d-3e8e7ec6e082"), new LanguageCode("en"), new LanguageName("Английский"));

    /// <summary>
    /// Возвращает язык: Russian.
    /// </summary>
    public static readonly LanguageDictionary Russian = new(new Guid("3a156e1f-6090-39cd-7580-20395231a00f"), new LanguageCode("ru"), new LanguageName("Русский"));

    private static readonly Dictionary<LanguageCode, LanguageDictionary> Languages = new()
    {
        [Russian.Code] = Russian,
        [English.Code] = English
    };

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LanguageDictionary" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код языка.</param>
    /// <param name="name">Наименование языка.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    public LanguageDictionary(SequentialGuid id, LanguageCode code, LanguageName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код языка.
    /// </summary>
    public LanguageCode Code { get; }

    /// <summary>
    /// Возвращает наименование языка.
    /// </summary>
    public LanguageName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления языков.
    /// </summary>
    public static IReadOnlyCollection<LanguageDictionary> GetAllValues() => Languages.Values;
}
