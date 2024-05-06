namespace LootBoot.Epilogue.Game.Characteristics;
public struct DoubleLimit
{
    public static implicit operator (double minimum, double maximum)(DoubleLimit limit) => (limit.Minimum, limit.Maximum);
    public static implicit operator DoubleLimit((double minimum, double maximum) value) => new(value.minimum, value.maximum);
    public static implicit operator DoubleLimit(double value) => new(value, value);
    public DoubleLimit(double minimum, double maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
        if (Minimum > Maximum)
        {
            throw new Exception($"The minimum value {Minimum} cannot be larger than the maximum value {Maximum}");
        }
    }
    public double Minimum { get; }
    public double Maximum { get; }
}
