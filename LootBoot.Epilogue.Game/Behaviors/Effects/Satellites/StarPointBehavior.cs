namespace LootBoot.Epilogue.Game.Behaviors.Effects.Satellites;
public class StarPointBehavior : SatelliteBehavior
{
    private const double DIAMETER_MIN = 0.001;
    private const double DIAMETER_MAX = 0.003;
    private const double DIAMETER_PROBABILITY_POWER = 25;
    private const byte COLOR_R = 255;
    private const byte COLOR_G = 255;
    private const byte COLOR_B_MAX = 255;
    //private const double SCALE_MIN = 0.1;
    //private const double SCALE_MAX = 1;
    //private double scaleMin;
    //private double scaleMax;
    //private bool oscillate;
    //private bool increase;
    //private double scaleRate;
    protected override void Create()
    {
        //Rotation = GetScript<Rotation>();
        //Rotation.Set(new Rotation.State
        //{
        //    Value = Rng.ToDouble(0, 50),
        //});
        Shape = GetScript<Shape>();
        base.Create();
    }
    protected override void Generate()
    {
        base.Generate();
        //scaleRate = Rng.ToDouble(0.005, 0.1);
        Size size = Geometry.Size;
        double sizePercentage = Calculations.Percentage.From(size.Width, DIAMETER_MAX);
        byte colorB = (byte)(COLOR_B_MAX - Calculations.Percentage.Of(sizePercentage, COLOR_B_MAX));
        Shape.Color = new Color(COLOR_R, COLOR_G, colorB);
        //scaleMin = Sprite.SizeScale;
        //scaleMax = Sprite.SizeScale + Rng.ToDouble(SCALE_MIN, SCALE_MAX);
        //oscillate = true;
        //increase = true;
    }
    protected override void Update()
    {
        //if (oscillate)
        //{
        //    double scale = Sprite.SizeScale;
        //    if (increase)
        //    {
        //        scale += scaleRate;
        //        if (scale >= scaleMax)
        //        {
        //            scale = scaleMax;
        //            increase = false;
        //        }
        //    }
        //    else
        //    {
        //        scale -= scaleRate;
        //        if (scale <= scaleMin)
        //        {
        //            scale = scaleMin;
        //            increase = true;
        //        }
        //    }
        //    Sprite.SizeScale = scale;
        //}
        base.Update();
    }
    protected override double OutsideOffsetArea() => 0;
    protected override Size GenerateSize()
    {
        Size size = new PVSize(Rng.ToDouble(DIAMETER_MIN, DIAMETER_MAX));
        return size;
    }
    protected override double GeneratePerspectiveScale()
    {
        Size size = Geometry.Size;
        double scale = size.Width * 0.01;
        return scale;
    }
    protected override Size GetMaximumSize() => new PVSize(DIAMETER_MAX);
    protected Shape Shape { get; private set; }
    //protected Rotation Rotation { get; private set; }
}
