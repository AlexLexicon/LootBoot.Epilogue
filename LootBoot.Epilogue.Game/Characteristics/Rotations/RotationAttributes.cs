namespace LootBoot.Epilogue.Game.Characteristics.Rotations;
public class RotationAttributes : Attributes<RotationLimits, RotationStats>
{
    public RotationAttributes(Script parent, RotationStats stats, RotationLimits limits) : base(parent, stats, limits)
    {
        MinMax = Math.Abs(Stats.MinMax.GetAscending(Limits.MinMax));
        Acceleration = Stats.Acceleration.GetAscending(Limits.Acceleration);
        Momentum = Stats.Momentum.GetAscending(Limits.Momentum);
    }
    public double MinMax { get; }
    public double Acceleration { get; }
    public double Momentum { get; }
}
