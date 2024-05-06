namespace LootBoot.Epilogue.Game;
public static class Extensions
{
    public static Point Percentage(this Size size, double xPercentage, double yPercentage) => new Point(Calculations.Percentage.Of(xPercentage, size.Width), Calculations.Percentage.Of(yPercentage, size.Height));
    public static void Set(this Velocity velocity, VelocityAttributes attributes)
    {
        velocity.Maximum = attributes.Maximum;
        velocity.Minimum = attributes.Minimum;
        velocity.Acceleration = attributes.Acceleration;
        velocity.Momentum = attributes.Momentum;
    }
    public static void Set(this Rotation rotation, RotationAttributes attributes)
    {
        rotation.Maximum = attributes.MinMax;
        rotation.Minimum = -attributes.MinMax;
        rotation.Acceleration = attributes.Acceleration;
        rotation.Momentum = attributes.Momentum;
    }
    public static Color GetColor(this Rarity rarity) => rarity switch
    {
        Rarity.Rare => new Color(73, 179, 255),
        Rarity.Remarkable => new Color(178, 109, 255),
        Rarity.Master => new Color(255, 151, 25),
        _ => Color.White,
    };
    public static Rarity GetRarity(this Traits.Weapons trait) => (Rarity)trait;
    public static Rarity GetRarity(this Traits.Fuselages trait) => (Rarity)trait;
    public static Rarity GetRarity(this Traits.Modifications trait) => (Rarity)trait;
    public static double GetAscending(this Tier tier, DoubleLimit limits)
    {
        double multiplier = (limits.Maximum - limits.Minimum) / (Tier.MAXIMUM - Tier.MINIMUM);
        double result = multiplier * (tier - 1);
        return result + limits.Minimum;
    }
    public static double GetDescending(this Tier tier, DoubleLimit limits)
    {
        tier = Tier.MAXIMUM + 1 - tier;
        return GetAscending(tier, limits);
    }
}
