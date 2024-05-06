namespace LootBoot.Epilogue.Game.Characteristics.Weapons;
public struct WeaponLimits
{
    public double PVSizeWidthScale { get; init; }
    public DoubleLimit Accuracy { get; init; }
    public DoubleLimit Velocity { get; init; }
    public DoubleLimit FireRate { get; init; }
}
