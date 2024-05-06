namespace LootBoot.Epilogue.Engine.GeometricProperties;
public class GeometricSize : GeometricProperty<Size>
{
    public static implicit operator Size(GeometricSize geometricPosition) => geometricPosition is not null ? geometricPosition.World : default;
    public static implicit operator GeometricSize(Size value) => new() { Value = value };
    internal GeometricSize() { }
    protected override Size GetDerivedValue()
    {
        Size thisSize = Value;
        if (BaseGeometry is null)
        {
            return Value;
        }
        Size baseSize = BaseGeometry.Size;
        Size derivedSize = new(thisSize.Width + baseSize.Width, thisSize.Height + baseSize.Height);
        return derivedSize;
    }
    protected override Size GetScreenValue() => World;
}
