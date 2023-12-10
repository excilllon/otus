namespace Lesson13
{
    /// <summary>
    /// Реализация хеш-таблицы
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class HashMap<K, V>
    {
        private const double DefLoadFactor = 0.75;
        private const int DefCapacity = 15;

        private HashEntry<K, V>[] _buckets;
        private int _capacity;
        private int _size;

        public HashMap()
        {
            _buckets = new HashEntry<K, V>[DefCapacity];
        }

        public HashMap(int capacity)
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
            int indexInBucket = Hash(key);
            var currEntry = _buckets[indexInBucket];
            if(currEntry == null) return;

            if (currEntry.Next == null)
            {
                _buckets[indexInBucket] = null;
                _size--;
                return;
            }

            HashEntry<K, V> prevEntry = null;
            while (currEntry != null)
            {
                if (currEntry.Key.Equals(key))
                {
                    if(prevEntry == null) _buckets[indexInBucket] = currEntry.Next;
                    else prevEntry.Next = currEntry.Next;
                    _size--;
                    return;
                }
                prevEntry = currEntry;
                currEntry = currEntry.Next;
            }
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
                int indexInBucket = Hash(key);
                var entry = _buckets[indexInBucket];
                while (entry != null)
                {
                    if(entry.Key.Equals(key))
                        return entry.Value;
                    entry = entry.Next;
                }
                return default;
            }
            
            set
            {
                int indexInBucket = Hash(key);
                var entry = _buckets[indexInBucket];
                HashEntry<K, V> lastEntryInChain = null;
                while (entry != null)
                {
                    // если такой ключ есть - обновляем значение
                    if (entry.Key.Equals(key))
                    {
                        entry.Value = value;
                        return;
                    }
                    lastEntryInChain = entry;
                    entry = entry.Next;
                }

                if (++_size > _buckets.Length * DefLoadFactor)
                {
                    Rehash();
                }
                // если ключа нет, создаем
                if (lastEntryInChain != null)
                {
                    lastEntryInChain.Next = new HashEntry<K, V>(key, value);
                    return;
                }
                _buckets[indexInBucket] = new HashEntry<K, V>(key, value);
            }
        }

        public int Capacity => _buckets.Length;

        private void Rehash()
        {
            var oldBuckets = _buckets;
            _buckets = new HashEntry<K, V>[oldBuckets.Length * 2 + 1];

            foreach (var srcEntry in oldBuckets)
            {
                var listSrcEntry = srcEntry;
                while (listSrcEntry != null)
                {
                    int indexInBucket = Hash(listSrcEntry.Key);
                    var destEntry = _buckets[indexInBucket];
                    if (destEntry == null)
                    {
                        _buckets[indexInBucket] = listSrcEntry;
                    }
                    else
                    {
                        var nextDest = destEntry.Next;
                        while (nextDest != null)
                        {
                            destEntry = nextDest;
                            nextDest = destEntry.Next;
                        }
                        destEntry.Next = listSrcEntry;
                    }

                    listSrcEntry = srcEntry.Next;
                    srcEntry.Next = null;
                }
            }
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
                var curEntry = entry;
                while (curEntry != null)
                {
                    if (curEntry.Value.Equals(value)) return true;
                }
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
            int indexInBucket = Hash(key);
            var entry = _buckets[indexInBucket];
            while (entry != null)
            {
                if(entry.Key.Equals(key))
                    return true;
                entry = entry.Next;
            }
            return false;
        }

        /// <summary>
        /// Очистка 
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _buckets = new HashEntry<K, V>[_capacity];
        }

        protected virtual int Hash(K value)
        {
            return Math.Abs(value.GetHashCode() % _buckets.Length);
        }

        private class HashEntry<K, V>
        {
            public HashEntry(K key, V value)
            {
                Key = key;
                Value = value;
            }
            public HashEntry<K, V>? Next { get; set; }
            public K Key { get; set; }
            public V Value { get; set; }
        }
    }
}
