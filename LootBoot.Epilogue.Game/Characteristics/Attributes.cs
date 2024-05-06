namespace LootBoot.Epilogue.Game.Characteristics;
public abstract class Attributes<TLimits, TStats> : Script where TLimits : struct where TStats : struct
{
    public Attributes(Script parent, TStats stats, TLimits limits) : base(parent)
    {
        Stats = stats;
        Limits = limits;
    }
    public TStats Stats { get; }
    protected TLimits Limits { get; }
}
