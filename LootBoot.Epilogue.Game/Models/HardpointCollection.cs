namespace LootBoot.Epilogue.Game.Models;
public class HardpointCollection : IEnumerable<KeyValuePair<int, Hardpoint[]>>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<int, Hardpoint[]>> GetEnumerator() => Points.GetEnumerator();
    public HardpointCollection() => Points = new Dictionary<int, Hardpoint[]>();
    public void Add(Hardpoint[] points)
    {
        if (points is not null)
        {
            int count = points.Length;
            if (Points.ContainsKey(count))
            {
                Points[count] = points;
            }
            else
            {
                Points.Add(count, points);
            }
        }
    }
    public Hardpoint[] this[int key] => Points.ContainsKey(key) ? Points[key] : null;
    public IEnumerable<int> Keys => Points.Keys;
    public IEnumerable<Hardpoint[]> Values => Points.Values;
    private Dictionary<int, Hardpoint[]> Points { get; }
}
