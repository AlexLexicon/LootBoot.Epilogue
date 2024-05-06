namespace LootBoot.Epilogue.Game.Behaviors.Effects.Satellites;
public class StarStreakBehavior : SatelliteBehavior
{
    private const double OPACITY_MIN = 0.3;
    private const double WIDTH_MIN = 1;
    private const double WIDTH_MAX = 1;
    private const double HEIGHT_MIN = 2;
    private const double HEIGHT_MAX = 40;
    private const double HEIGHT_MAX_FTL = 100;
    protected override void Create()
    {
        base.Create();
        Shape = GetScript<Shape>("Sprite");
        Testing = GetScript<Shape>("Testing");
        Testing.PerspectiveScale = savedScale;
    }
    protected override void Update()
    {
        if (Conductor is not null)
        {
            double velocity = Math.Abs(Conductor.Velocity.Value);
            double maximumVelocity = Conductor.Velocity.Maximum is null ? velocity : Conductor.Velocity.Maximum.Value;
            double velocityPercentage = Calculations.Percentage.From(velocity, maximumVelocity);
            Shape.Opacity = OPACITY_MIN + Calculations.Percentage.Of(velocityPercentage, 1 - OPACITY_MIN);
            double height = HEIGHT_MIN + Calculations.Percentage.Of(velocityPercentage, (StarStreak.IsFTL ? HEIGHT_MAX_FTL : HEIGHT_MAX) - HEIGHT_MIN);
            Size size = Geometry.Size;
            Geometry.Size = new Size(size.Width, height);
            Geometry.Angle = Conductor.Geometry.Angle;
        }
        base.Update();
    }
    protected override Size GenerateSize()
    {
        Geometry.Angle = 135;
        double width = Rng.ToDouble(WIDTH_MIN, WIDTH_MAX);
        Size size = new Size(width, HEIGHT_MIN);
        return GetMaximumSize();
    }
    protected override double OutsideOffsetArea()
    {
        double? value = Conductor.Velocity.Value;
        return value is null ? 0 : Math.Abs(value.Value);
    }
    private double savedScale;
    protected override double GeneratePerspectiveScale()
    {
        double scale = Rng.ToDouble(0.8, 1);
        savedScale = scale;
        return scale;
    }
    protected override Size GetMaximumSize() => new Size(WIDTH_MAX, HEIGHT_MAX_FTL);
    protected Shape Shape { get; private set; }
    protected Shape Testing { get; private set; }
    protected Entity Conductor => StarStreak.Conductor;
}
