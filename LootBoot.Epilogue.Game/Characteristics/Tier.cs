namespace LootBoot.Epilogue.Game.Characteristics;
public struct Tier
{
    public const uint MINIMUM = 1;
    public const uint MAXIMUM = 9;
    public static implicit operator uint(Tier tier) => tier.Value;
    public static implicit operator Tier(uint value) => new(value);
    public Tier(uint value)
    {
        _value = MINIMUM;
        Value = value;
    }
    private uint? _value;
    public uint Value
    {
        get => _value is null ? MINIMUM : _value.Value;
        init => _value = Math.Clamp(value, MINIMUM, MAXIMUM);
    }
}
