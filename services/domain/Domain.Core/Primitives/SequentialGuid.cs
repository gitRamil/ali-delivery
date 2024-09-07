using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Domain.Core.Primitives;

/// <summary>
/// Представляет последовательно генерируемый идентификатор.
/// </summary>
[DebuggerDisplay("{" + nameof(_value) + "}")]
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct SequentialGuid : IEquatable<SequentialGuid>, IComparable<SequentialGuid>, IComparable, ISpanFormattable, ISpanParsable<SequentialGuid>
{
    /// <summary>
    /// Возвращает доступный только для чтения экземпляр структуры <see cref="SequentialGuid" />, значением которого являются
    /// все ноли.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once NotAccessedField.Global
    public static readonly SequentialGuid Empty = new(Guid.Empty);

    private static long _timestamp;
    private static readonly object Lock = new();
    private readonly Guid _value;

    private SequentialGuid(Guid value) => _value = value;

    /// <summary>
    /// Создает последовательно генерируемый идентификатор.
    /// </summary>
    /// <returns>Последовательно генерируемый идентификатор.</returns>
    public static SequentialGuid Create()
    {
        var randomBytes = new byte[10];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomBytes);
        var timestamp = GetTimestamp();
        var timestampBytes = BitConverter.GetBytes(timestamp);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        var guidBytes = new byte[16];
        Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
        Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
        }

        return new SequentialGuid(new Guid(guidBytes));
    }

    /// <summary>
    /// Возвращает 16-элементный байтовый массив, содержащий значение этого экземпляра.
    /// </summary>
    /// <returns>16-элементный байтовый массив.</returns>
    public byte[] ToByteArray() => _value.ToByteArray();

    /// <summary>
    /// Возвращает строковое представление значения этого экземпляра <see cref="SequentialGuid" /> в соответствии с
    /// предоставленным спецификатором формата.
    /// </summary>
    /// <param name="format">
    /// Единый спецификатор формата, который указывает, как форматировать значение этого
    /// <see cref="SequentialGuid" />. Значением параметра <paramref name="format" /> может быть "N", "D", "B", "P" или "X".
    /// Если <paramref name="format" /> является <c>null</c> или пустой строкой (""), используется "D".
    /// </param>
    /// <returns>
    /// Значение этого <see cref="SequentialGuid" />, представленное в виде ряда строчных шестнадцатеричных цифр в указанном
    /// формате.
    /// </returns>
    public string ToString([StringSyntax(StringSyntaxAttribute.GuidFormat)] string? format) => _value.ToString(format);

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj is SequentialGuid other ? CompareTo(other) : throw new ArgumentException($"Объект должен быть типом {nameof(SequentialGuid)}");
    }

    /// <inheritdoc />
    public int CompareTo(SequentialGuid other) => string.Compare(_value.ToString(), other._value.ToString(), StringComparison.Ordinal);

    /// <summary>
    /// Определяет, равен ли указанный <paramref name="other" /> этому экземпляру.
    /// </summary>
    /// <param name="other">Объект для сравнения.</param>
    /// <returns><c>true</c> Если <paramref name="other" /> равен текущему экземпляру; в противном случае <c>false</c>.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool Equals(SequentialGuid other) => _value.Equals(other._value);

    /// <inheritdoc />
    public static SequentialGuid Parse(string s, IFormatProvider? provider) => new(Guid.Parse(s, provider));

    /// <inheritdoc />
    public static SequentialGuid Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => new(Guid.Parse(s, provider));

    /// <inheritdoc />
    public string ToString([StringSyntax(StringSyntaxAttribute.GuidFormat)] string? format, IFormatProvider? formatProvider) => _value.ToString(format, formatProvider);

    /// <inheritdoc />
    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) =>
        ((ISpanFormattable)_value).TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc />
    public static bool TryParse(string? s, IFormatProvider? provider, out SequentialGuid result)
    {
        var value = Guid.TryParse(s, provider, out var guid);
        result = new SequentialGuid(guid);
        return value;
    }

    /// <inheritdoc />
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out SequentialGuid result)
    {
        var value = Guid.TryParse(s, provider, out var guid);
        result = new SequentialGuid(guid);
        return value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is SequentialGuid other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => _value.ToString();

    private static long GetTimestamp()
    {
        lock (Lock)
        {
            var timestamp = DateTime.UtcNow.Ticks / 10000L;

            while (_timestamp == timestamp)
            {
                timestamp = DateTime.UtcNow.Ticks / 10000L;
            }

            _timestamp = timestamp;
            return timestamp;
        }
    }

    /// <summary>
    /// Реализует оператор ==.
    /// </summary>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    /// <returns>
    /// Возвращает результат операции.
    /// </returns>
    public static bool operator ==(SequentialGuid left, SequentialGuid right) => left.Equals(right);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="SequentialGuid" /> в <see cref="Guid" />.
    /// </summary>
    /// <param name="guid">Уникальный идентификатор.</param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static implicit operator Guid(SequentialGuid guid) => guid._value;

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="Guid" /> в <see cref="SequentialGuid" />.
    /// </summary>
    /// <param name="guid">Уникальный идентификатор.</param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static implicit operator SequentialGuid(Guid guid) => new(guid);

    /// <summary>
    /// Реализует оператор !=.
    /// </summary>
    /// <param name="left">Левый операнд.</param>
    /// <param name="right">Правый операнд.</param>
    /// <returns>
    /// Возвращает результат операции.
    /// </returns>
    public static bool operator !=(SequentialGuid left, SequentialGuid right) => !left.Equals(right);
}
