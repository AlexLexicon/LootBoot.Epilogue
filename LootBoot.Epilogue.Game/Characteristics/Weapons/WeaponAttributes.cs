namespace LootBoot.Epilogue.Game.Characteristics.Weapons;
public class WeaponAttributes : Attributes<WeaponLimits, WeaponStats>
{
    public WeaponAttributes(Script parent, WeaponStats stats, WeaponLimits limits) : base(parent, stats, limits)
    {
        Accuracy = Stats.Accuracy.GetDescending(Limits.Accuracy);
        Velocity = Stats.Velocity.GetAscending(Limits.Velocity);
        FireRate = Stats.FireRate.GetDescending(Limits.FireRate);
    }
    public double Accuracy { get; }
    public double Velocity { get; }
    public double FireRate { get; }
}
