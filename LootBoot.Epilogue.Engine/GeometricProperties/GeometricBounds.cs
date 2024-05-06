namespace LootBoot.Epilogue.Engine.GeometricProperties;
public class GeometricBounds : GeometricProperty<Rect>
{
    public static implicit operator Rect(GeometricBounds geometricPosition) => geometricPosition is not null ? geometricPosition.World : default;
    internal GeometricBounds() { }
    public override Rect Value
    {
        get
        {
            if (Geometry is null)
            {
                return default;
            }
            Size thisSize = Geometry.Size.Value;
            Point thisPosition = Geometry.Position.Value;
            double valueX = thisPosition.X - thisSize.Width / 2;
            double valueY = thisPosition.Y - thisSize.Height / 2;
            Rect valueBounds = new(new Point(valueX, valueY), thisSize);
            return valueBounds;
        }
    }
    protected override Rect GetDerivedValue()
    {
        if (Geometry is null)
        {
            return default;
        }
        if (BaseGeometry is null)
        {
            return Value;
        }
        Size thisSize = Geometry.Size.Value;
        Point thisPosition = Geometry.Position.Value;
        Size baseSize = BaseGeometry.Size;
        Point basePosition = BaseGeometry.Position;
        double derivedWidth = thisSize.Width + baseSize.Width;
        double derivedHeight = thisSize.Height + baseSize.Height;
        double derivedX = thisPosition.X + basePosition.X - derivedWidth / 2;
        double derivedY = thisPosition.Y + basePosition.Y - derivedHeight / 2;
        Rect derivedBounds = new(new Point(derivedX, derivedY), new Size(derivedWidth, derivedHeight));
        return derivedBounds;
    }
    protected override Rect GetScreenValue()
    {
        Rect worldBounds = World;
        Point screenPosition = worldBounds.Location.ToScreen();
        Rect screenBounds = new(screenPosition, worldBounds.Size);
        return screenBounds;
    }
}
