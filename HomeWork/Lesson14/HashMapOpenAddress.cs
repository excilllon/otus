namespace Lesson14
{
    /// <summary>
    /// Хэш-таблица с открытой адресацией
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class HashMapOpenAddress<K, V>
    {
        private const double DefLoadFactor = 0.5;
        private const int DefCapacity = 15;

        private HashEntry<K, V>[] _buckets;
        private int _capacity;
        private int _size;

        public HashMapOpenAddress(): this(DefCapacity)
        {
        }

        public HashMapOpenAddress(int capacity)
        {
            if (capacity <= 0) throw new ArgumentException();
            _buckets = new HashEntry<K, V>[capacity];
            _capacity = capacity;
        }

        /// <summary>
        /// Удаление элемента по ключу
        /// </summary>
        /// <param name="key"></param>
        public void Remove(K key)
        {
            var entry = Get(key);
            if (entry == null) return;
            entry.IsDeleted = true;
            _size--;
        }

        /// <summary>
        /// Добавление, чтение и запись элемента по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public V this[K key]
        {
            get
            {
                var entry = Get(key);
                return entry == null ? default : entry.Value;
            }

            set
            {
                if (_size + 1 > Capacity * DefLoadFactor)
                {
                    Rehash();
                }
                CreateOrUpdate(key, value);
            }
        }

        public int Capacity => _buckets.Length;

        protected virtual void Rehash()
        {
            var oldBuckets = _buckets;
            _buckets = new HashEntry<K, V>[GetNewCapacity()];
            _size = 0;
            foreach (var oldSrcEntry in oldBuckets)
            {
                if (oldSrcEntry == null || oldSrcEntry.IsDeleted) continue;

                CreateOrUpdate(oldSrcEntry.Key, oldSrcEntry.Value);
            }
        }

        protected int GetNewCapacity()
        {
            return _buckets.Length * 3 + 1;
        }

        /// <summary>
        /// Содержит ли значение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(V value)
        {
            foreach (var entry in _buckets)
            {
                if (entry?.Value.Equals(value) == true) return true;
            }

            return false;
        }

        /// <summary>
        /// Содержит ли ключ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(K key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// Очистка 
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _buckets = new HashEntry<K, V>[_capacity];
        }

        /// <summary>
        /// Пробинг для получения HashEntry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private HashEntry<K, V>? Get(K key)
        {
            int i = 0;
            do
            {
                var idx = ProbeHash(key, i);
                if (_buckets[idx] != null &&
                    _buckets[idx].Key.Equals(key) && !_buckets[idx].IsDeleted) return _buckets[idx];
            }
            while (++i < _buckets.Length);
            return null;
        }

        /// <summary>
        /// Пробинг с созданием/обновление HashEntry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void CreateOrUpdate(K key, V value)
        {
            while (true)
            {
                int i = 0;
                HashEntry<K, V> emptyEntry = null;
                int idx = 0;
                do
                {
                    idx = ProbeHash(key, i);
                    if (_buckets[idx] == null) break;
                    if (_buckets[idx].Key.Equals(key))
                    {
                        _buckets[idx].Value = value;
                        return;
                    }

                    if (_buckets[idx].IsDeleted && emptyEntry == null) emptyEntry = _buckets[idx];
                } while (++i < _buckets.Length);

                if (emptyEntry == null && _buckets[idx] == null)
                {
                    _buckets[idx] = new HashEntry<K, V>(key, value);
                    _size++;
                    return;
                }
                if (emptyEntry != null)
                {
                    emptyEntry.Key = key;
                    emptyEntry.Value = value;
                    emptyEntry.IsDeleted = false;
                    return;
                }
                // если не нашли свободной ячейки и прошли весь массив - рехэшируем
                Rehash();
            }
        }

        protected virtual int ProbeHash(K key, int i)
        {
            return (Math.Abs(key.GetHashCode()) % Capacity + i) % Capacity;
        }

        protected class HashEntry<K, V>
        {
            public HashEntry(K key, V value)
            {
                Key = key;
                Value = value;
            }
            public K Key { get; set; }
            public V Value { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
