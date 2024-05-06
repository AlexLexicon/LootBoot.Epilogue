namespace LootBoot.Epilogue.Game.Characteristics.Fuselages;
public struct FuselageStats
{
    public Makes.Fuselages Make { get; init; }
    public FuselageTraits Traits { get; init; }
    public TechStat Tech { get; init; }
    public Tier Integrity { get; init; }
    public VelocityStats Velocity { get; init; }
    public RotationStats Rotation { get; init; }
}
