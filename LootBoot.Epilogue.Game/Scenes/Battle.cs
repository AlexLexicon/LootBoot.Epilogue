namespace LootBoot.Epilogue.Game.Scenes;
public abstract class Battle : Scene
{
    public delegate void BattleChangedDelegate(int total, int remaining);
    public event Action OnStart;
    public event Action OnComplete;
    public event Action OnWaveComplete;
    public event Action OnWaveStart;
    public event BattleChangedDelegate OnWavesChange;
    public event BattleChangedDelegate OnWaveFactorsChange;
    public Battle() : base((int)Configurations.Scenes.Layers.Battle)
    {
        Waves = GenerateWaves();
        Controller = new BattleController(this);
        Controller.When<BattleBehavior>().IsCompleted().Run(() => OnComplete?.Invoke());
    }
    public void Start()
    {
        IsActive = true;
        OnStart?.Invoke();
    }
    public bool SetCurrentWave(int index)
    {
        bool isComplete = true;
        int total = 0;
        int remaining = 0;
        if (Waves is not null)
        {
            total = Waves.Count;
            if (index >= 0 && Waves is not null && index < total)
            {
                CurrentWave = Waves[index];
                if (CurrentWave is not null)
                {
                    isComplete = CurrentWave.IsComplete;
                    CurrentWave.Changed += WaveFactorsChange;
                    CurrentWave.Completed += () => OnWaveComplete?.Invoke();
                }
                remaining = total - index;
            }
        }
        OnWavesChange?.Invoke(total, remaining);
        return isComplete;
    }
    public void StartCurrentWave()
    {
        CurrentWave.Start();
        OnWaveStart?.Invoke();
    }
    protected virtual void WaveFactorsChange(int totalFactors, int remainingFactors) => OnWaveFactorsChange?.Invoke(totalFactors, remainingFactors);
    public abstract List<Wave> GenerateWaves();
    public Wave CurrentWave { get; private set; }
    public List<Wave> Waves { get; }
    public Controller Controller { get; }
    public bool IsActive { get; private set; }
}
