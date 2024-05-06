namespace LootBoot.Epilogue.Game.Characteristics.Fuselages;
public class FuselageAttributes : Attributes<FuselageLimits, FuselageStats>
{
    public FuselageAttributes(Script parent, FuselageStats stats, FuselageLimits limits) : base(parent, stats, limits)
    {
        Velocity = new VelocityAttributes(this, stats.Velocity, Limits.Velocity);
        Rotation = new RotationAttributes(this, stats.Rotation, Limits.Rotation);
    }
    public VelocityAttributes Velocity { get; }
    public RotationAttributes Rotation { get; }
}
