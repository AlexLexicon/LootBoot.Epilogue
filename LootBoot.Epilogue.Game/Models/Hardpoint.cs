namespace LootBoot.Epilogue.Game.Models;
public class Hardpoint
{
    public Hardpoint(double centerXPercentage, double centerYPercentage, double barrelXPercentage, double barrelYPercentage)
    {
        CenterXPercentage = centerXPercentage;
        CenterYPercentage = centerYPercentage;
        BarrelXPercentage = barrelXPercentage;
        BarrelYPercentage = barrelYPercentage;
    }
    public Point GetCenterValue(double? scale = null)
    {
        Size size = scale is not null ? Size.Scale(scale.Value) : Size;
        return size.Percentage(CenterXPercentage, CenterYPercentage);
    }
    public Point GetBarrelValue(double? scale = null)
    {
        Size size = scale is not null ? Size.Scale(scale.Value) : Size;
        return size.Percentage(BarrelXPercentage, BarrelYPercentage);
    }
    public Point GetBarrel(Geometry geometry, double? scale = null)
    {
        Point barrelPosition = GetBarrelValue(scale);
        Point position = geometry.Position;
        Point spawnPosition = new(position.X + barrelPosition.X, position.Y + barrelPosition.Y);
        Rect bounds = Geometry.Bounds;
        return Calculations.Geometry.Position.FromAngle(spawnPosition, geometry.Angle, bounds.ToCenter());
    }
    protected Size Size => Geometry.Size;
    public Geometry Geometry { get; set; }
    public double CenterXPercentage { get; }
    public double CenterYPercentage { get; }
    public double BarrelXPercentage { get; }
    public double BarrelYPercentage { get; }
}
