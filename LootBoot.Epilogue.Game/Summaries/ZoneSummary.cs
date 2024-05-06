namespace LootBoot.Epilogue.Game.Summaries;
public class ZoneSummary : IEnumerable<KeyValuePair<SummaryDefinition, uint>>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<SummaryDefinition, uint>> GetEnumerator() => GetCounts().GetEnumerator();
    public ZoneSummary(uint zoneIndex)
    {
        ZoneIndex = zoneIndex;
        Counts = new Dictionary<SummaryDefinition, uint>();
    }
    public void Add(SummaryDefinition summary)
    {
        if (Counts.ContainsKey(summary))
        {
            uint total = Counts[summary];
            total++;
            Counts[summary] = total;
        }
        else
        {
            Counts.Add(summary, 1);
        }
    }
    public IEnumerable<KeyValuePair<SummaryDefinition, uint>> GetCounts() => Counts;
    protected Dictionary<SummaryDefinition, uint> Counts { get; set; }
    public uint ZoneIndex { get; }
}
