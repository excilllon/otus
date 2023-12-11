namespace Lesson14;

/// <summary>
/// Хэш-таблица с квадратичным пробингом
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
public sealed class HashMapQuadraticProbe<K, V> : HashMapOpenAddress<K, V>
{
    private int bCoef = 1;

    public HashMapQuadraticProbe(int capacity): base(capacity)
    {
        // Ищем не делитель N
        while (Capacity % ++bCoef == 0)
        {
        }
    }

    protected override void Rehash()
    {
        var newCapacity = GetNewCapacity();
        bCoef = 1;
        while (newCapacity % ++bCoef == 0)
        {
        }
        base.Rehash();
    }

    protected override int ProbeHash(K key, int i)
    {
        return (Math.Abs(key.GetHashCode()) % Capacity + i * i * bCoef) % Capacity;
    }

}