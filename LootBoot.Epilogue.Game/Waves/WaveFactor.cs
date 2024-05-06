namespace LootBoot.Epilogue.Game.Waves;
public abstract class WaveFactor<TGenerateDefinition> : WaveFactor
{
    public WaveFactor(Scene parent, GameTeam team) : base(parent, team) { }
    public abstract void Generate(TGenerateDefinition definition);
}
public abstract class WaveFactor : Actor
{
    public event Action OnWaveStepComplete;
    public WaveFactor(Scene parent, GameTeam team) : base(parent, team) { }
    public new GameTeam Team => (GameTeam)base.Team;
    protected virtual void WaveStepComplete() => OnWaveStepComplete?.Invoke();
}
