namespace LootBoot.Epilogue.Engine.GeometricProperties;
public sealed class GeometricAngle : GeometricProperty<Angle>
{
    public static implicit operator Angle(GeometricAngle geometricAngle) => geometricAngle is not null ? geometricAngle.World : default;
    public static implicit operator int(GeometricAngle geometricAngle) => geometricAngle is not null ? geometricAngle.World : default;
    public static implicit operator uint(GeometricAngle geometricAngle) => geometricAngle is not null ? geometricAngle.World : default;
    public static implicit operator float(GeometricAngle geometricAngle) => geometricAngle is not null ? geometricAngle.World : default;
    public static implicit operator double(GeometricAngle geometricAngle) => geometricAngle is not null ? geometricAngle.World : default;
    public static implicit operator GeometricAngle(Angle value) => new() { Value = value };
    public static implicit operator GeometricAngle(int degrees) => new() { Value = degrees };
    public static implicit operator GeometricAngle(uint degrees) => new() { Value = degrees };
    public static implicit operator GeometricAngle(float degrees) => new() { Value = degrees };
    public static implicit operator GeometricAngle(double degrees) => new() { Value = degrees };
    internal GeometricAngle() { }
    protected override Angle GetDerivedValue()
    {
        Angle thisAngle = Value;
        if (BaseGeometry is null)
        {
            return Value;
        }
        Angle baseAngle = BaseGeometry.Angle;
        Angle derivedAngle = thisAngle + baseAngle;
        return derivedAngle;
    }
    protected override Angle GetScreenValue() => World;
}
