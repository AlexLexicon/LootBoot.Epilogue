namespace LootBoot.Epilogue.Game.Characteristics.Weapons;
public struct WeaponStats
{
    public Makes.Weapons Make { get; init; }
    public WeaponTraits Traits { get; init; }
    public uint Count { get; init; }
    public TechStat Tech { get; init; }
    public Tier Damage { get; init; }
    public Tier Accuracy { get; init; }
    public Tier Velocity { get; init; }
    public Tier FireRate { get; init; }
}
