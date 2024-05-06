namespace LootBoot.Epilogue.Engine.GeometricProperties;
public class GeometricPosition : GeometricProperty<Point>
{
    public static implicit operator Point(GeometricPosition geometricPosition) => geometricPosition is not null ? geometricPosition.World : default;
    public static implicit operator GeometricPosition(Point value) => new() { Value = value };
    internal GeometricPosition() => IsRotatable = true;
    protected override Point GetDerivedValue()
    {
        Point thisPosition = Value;
        if (BaseGeometry is null)
        {
            return Value;
        }
        Point basePosition = BaseGeometry.Position;
        Point derivedPosition = new(thisPosition.X + basePosition.X, thisPosition.Y + basePosition.Y);
        if (IsRotatable)
        {
            Point finalPosition = derivedPosition.FromAngle(BaseGeometry.Angle, basePosition);
            return finalPosition;
        }
        return derivedPosition;
    }
    protected override Point GetScreenValue() => World.ToScreen();
    public bool IsRotatable { get; set; }
}
