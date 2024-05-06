namespace LootBoot.Epilogue.Game.Waves;
public class Wave : IEnumerable<WaveFactor>
{
    public delegate void ChangedDelegate(int total, int remaining);
    public event ChangedDelegate Changed;
    public event Action Completed;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public Wave(Battle battle)
    {
        Battle = battle;
        WaveDefinitions = new();
    }
    public IEnumerator<WaveFactor> GetEnumerator()
    {
        Start();
        return Factors.GetEnumerator();
    }
    public void Start()
    {
        if (!IsComplete && Factors is null)
        {
            Factors = new();
            foreach (WaveDefinition definition in WaveDefinitions)
            {
                for (int count = 0; count < definition.Count; count++)
                {
                    WaveFactor factor = definition.Create(Battle);
                    factor.OnWaveStepComplete += () => WaveStepComplete(factor);
                    Factors.Add(factor);
                }
            }
            TotalFactors = Factors.Count;
        }
        IsStarted = true;
        WaveStepComplete(null);
    }
    public void Add(WaveDefinition definition) => WaveDefinitions.Add(definition);
    private void WaveStepComplete(WaveFactor factor)
    {
        int remaining = 0;
        if (Factors is not null)
        {
            if (factor is not null && Factors.Contains(factor))
            {
                Factors.Remove(factor);
            }
            remaining = Factors.Count;
            if (remaining <= 0)
            {
                IsComplete = true;
                Completed?.Invoke();
                Factors = null;
            }
        }
        Changed?.Invoke(TotalFactors, remaining);
    }
    public HashSet<WaveFactor> RemainingFactors() => new HashSet<WaveFactor>(Factors);
    public Battle Battle { get; }
    public bool IsStarted { get; private set; }
    public bool IsComplete { get; private set; }
    private int TotalFactors { get; set; }
    private HashSet<WaveDefinition> WaveDefinitions { get; }
    private HashSet<WaveFactor> Factors { get; set; }
    public class WaveNotCreatedException : Exception
    {
        public WaveNotCreatedException() : base("The wave has not been created yet. Be sure to call the create method or GetEnumerator method before trying to iterate over the Wave")
        {
        }
    }
}
