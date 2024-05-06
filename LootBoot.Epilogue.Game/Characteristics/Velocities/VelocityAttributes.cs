namespace LootBoot.Epilogue.Game.Characteristics.Velocities;
public class VelocityAttributes : Attributes<VelocityLimits, VelocityStats>
{
    public VelocityAttributes(Script parent, VelocityStats stats, VelocityLimits limits) : base(parent, stats, limits)
    {
        Maximum = Stats.Maximum.GetAscending(Limits.Maximum);
        Minimum = Stats.Minimum.GetDescending(Limits.Minimum);
        Acceleration = Stats.Acceleration.GetAscending(Limits.Acceleration);
        Momentum = Stats.Momentum.GetAscending(Limits.Momentum);
    }
    public double Maximum { get; }
    public double Minimum { get; }
    public double Acceleration { get; }
    public double Momentum { get; }
}
