using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetDictionary;

/// <summary>
/// Представляет обработчик, получающий значения справочника.
/// </summary>
public sealed class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, IReadOnlyCollection<DictionaryDto>>
{
    private readonly IDictionaryValuesProvider _dictionaryValuesProvider;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetDictionaryQueryHandler" />.
    /// </summary>
    /// <param name="dictionaryValuesProvider">Провайдер значений справочника.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="dictionaryValuesProvider" /> равен <c>null</c>.
    /// </exception>
    public GetDictionaryQueryHandler(IDictionaryValuesProvider dictionaryValuesProvider) =>
        _dictionaryValuesProvider = dictionaryValuesProvider ?? throw new ArgumentNullException(nameof(dictionaryValuesProvider));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<IReadOnlyCollection<DictionaryDto>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var items = await _dictionaryValuesProvider.GetDictionaryValuesAsync(request.DictionaryCode, cancellationToken);

        var result = new List<DictionaryDto>();

        foreach (var item in items)
        {
            if (item is Role role && role.IsSystemRole())
            {
                continue;
            }

            var code = item.GetType()
                           .GetProperty("Name")
                           ?.GetValue(item)
                           ?.ToString();

            var name = item.GetType()
                           .GetProperty("Code")
                           ?.GetValue(item)
                           ?.ToString();

            if (code == null || name == null)
            {
                throw new InvalidOperationException($"Недопустимый код словаря: {code} - {name}");
            }

            result.Add(new DictionaryDto(code, name));
        }

        return result;
    }
}
