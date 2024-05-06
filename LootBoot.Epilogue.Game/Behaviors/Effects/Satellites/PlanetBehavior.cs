namespace LootBoot.Epilogue.Game.Behaviors.Effects.Satellites;
public class PlanetBehavior : SatelliteBehavior
{
    private const double DIAMETER_MIN = 0.1;
    private const double DIAMETER_MAX = 0.3;
    protected override void Create()
    {
        Shape = GetScript<Shape>();
        base.Create();
    }
    protected override void Generate()
    {
        base.Generate();
        Shape.IsVisible = Rng.Percentage.Chance(0.25);
        int r = Rng.ToInteger(0, 255);
        int g = Rng.ToInteger(0, 255);
        int b = Rng.ToInteger(0, 255);
        Shape.Color = new Color(r, g, b);
    }
    protected override Size GenerateSize()
    {
        Size size = new PVSize(Rng.ToDouble(0.1, 0.3));
        return size;
    }
    protected override double GeneratePerspectiveScale()
    {
        Size size = Geometry.Size;
        double scale = size.Width * 0.001;
        return scale;
    }
    protected override double OutsideOffsetArea() => 0;
    protected override Size GetMaximumSize() => new Size(DIAMETER_MAX, DIAMETER_MAX);
    protected Shape Shape { get; private set; }
}
