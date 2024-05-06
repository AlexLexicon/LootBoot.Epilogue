namespace LootBoot.Epilogue.Game.Characteristics.Velocities;
public struct VelocityLimits
{
    public DoubleLimit Maximum { get; init; }
    public DoubleLimit Minimum { get; init; }
    public DoubleLimit Acceleration { get; init; }
    public DoubleLimit Momentum { get; init; }
}
