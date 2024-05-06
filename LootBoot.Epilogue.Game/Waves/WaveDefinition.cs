namespace LootBoot.Epilogue.Game.Waves;
public class WaveDefinition<TWaveFactor, TGenerateDefinition> : WaveDefinition where TWaveFactor : WaveFactor<TGenerateDefinition>
{
    public override WaveFactor Create(Battle battle)
    {
        TWaveFactor factor = (TWaveFactor)Activator.CreateInstance(typeof(TWaveFactor), battle, Team);
        factor.Generate(GenerateDefinition);
        return factor;
    }
    public TGenerateDefinition GenerateDefinition { get; init; }
}
public abstract class WaveDefinition
{
    public abstract WaveFactor Create(Battle battle);
    public Team Team { get; init; }
    private int? _count;
    public int Count
    {
        get => _count is null ? 1 : _count.Value;
        init => _count = value;
    }
}
