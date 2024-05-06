namespace LootBoot.Epilogue.Engine.Utilties;
public class PairedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public PairedDictionary() => KeysSet = new List<KeyValuePair<TKey, TValue>>();
    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            throw new ArgumentException("Duplicate Key argument");
        }
        KeysSet.Add(new KeyValuePair<TKey, TValue>(key, value));
    }
    private IEnumerable<KeyValuePair<TKey, TValue>> Select(TKey key) => KeysSet.Where(kvp => kvp.Key is null ? key is null : kvp.Key.Equals(key));
    private IEnumerable<KeyValuePair<TKey, TValue>> Select(TValue value) => KeysSet.Where(kvp => kvp.Value is null ? value is null : kvp.Value.Equals(value));
    private bool ContainsKey(TKey key, out KeyValuePair<TKey, TValue> kvp)
    {
        HashSet<KeyValuePair<TKey, TValue>> set = Select(key).ToHashSet();
        if (set is not null && set.Count > 0)
        {
            kvp = set.First();
            return true;
        }
        kvp = default;
        return false;
    }
    private bool ContainsValue(TValue value, out HashSet<KeyValuePair<TKey, TValue>> kvps)
    {
        HashSet<KeyValuePair<TKey, TValue>> kvpsset = Select(value).ToHashSet();
        if (kvpsset is not null && kvpsset.Count > 0)
        {
            kvps = kvpsset;
            return true;
        }
        kvps = default;
        return false;
    }
    public bool ContainsKey(TKey key) => ContainsKey(key, out KeyValuePair<TKey, TValue> ignored);
    public bool ContainsValue(TValue value) => Select(value).Any();
    public bool Remove(TKey key)
    {
        if (ContainsKey(key, out KeyValuePair<TKey, TValue> kvp))
        {
            KeysSet.Remove(kvp);
            return true;
        }
        return false;
    }
    public bool TryGetKey(TValue value, [MaybeNullWhen(false)] out HashSet<TKey> keys)
    {
        if (ContainsValue(value, out HashSet<KeyValuePair<TKey, TValue>> kvps))
        {
            keys = kvps.Select(kvp => kvp.Key).ToHashSet();
            return true;
        }
        keys = default;
        return false;
    }
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (ContainsKey(key, out KeyValuePair<TKey, TValue> kvp))
        {
            value = kvp.Value;
            return true;
        }
        value = default;
        return false;
    }
    public TValue this[TKey key]
    {
        get => Select(key).First().Value;
        set
        {
            KeyValuePair<TKey, TValue> kvp = Select(key).First();
            KeysSet[KeysSet.IndexOf(kvp)] = new KeyValuePair<TKey, TValue>(key, value);
        }
    }
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
    public void Clear() => KeysSet.Clear();
    public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key, out KeyValuePair<TKey, TValue> kvp) && (item.Value is null ? kvp.Value is null : item.Value.Equals(kvp.Value));
    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => KeysSet.GetEnumerator();
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();
    public bool IsReadOnly => false;
    public int Count => KeysSet.Count;
    public ICollection<TKey> Keys => KeysSet.Select(kvp => kvp.Key).ToHashSet();
    public ICollection<TValue> Values => KeysSet.Select(kvp => kvp.Value).ToHashSet();
    private List<KeyValuePair<TKey, TValue>> KeysSet { get; }
}
