namespace LootBoot.Epilogue.Game.Characteristics;
public struct TechStat
{
    public const int MINIMUM = 0;
    public static implicit operator int(TechStat techStat) => techStat.Value;
    public static implicit operator TechStat(int value) => new(value);
    public static implicit operator TechStat(uint value)
    {
        int intValue;
        try
        {
            intValue = (int)value;
        }
        catch
        {
            intValue = int.MaxValue;
        }
        return new(intValue);
    }
    public TechStat(int value)
    {
        _value = MINIMUM;
        Value = value;
    }
    private int? _value;
    public int Value
    {
        get => _value is null ? MINIMUM : _value.Value;
        init => _value = Math.Max(value, MINIMUM);
    }
}
