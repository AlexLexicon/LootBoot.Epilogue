namespace LootBoot.Epilogue.Game.Entities.Effects.Satellites;
public sealed class StarStreak : Satellite<StarStreakBehavior>
{
    public static Entity Conductor { get; set; }
    public static bool IsFTL { get; set; }
    public StarStreak(Script parent) : base(parent)
    {
        Sprite = new Shape(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Satellites,
            Contour = Sprite.Contours.Rectangle,
            Color = Color.White,
        };
        Testing = new Shape(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Satellites + 1,
            Contour = Sprite.Contours.Rectangle,
            Color = Color.Red,
            IsVisible = false,
        };
        Testing.Geometry.Size.IsDerivable = false;
        Geometry.OnChange += (ea) =>
        {
            if (ea.IsSizeChanged)
            {
                Size size = Geometry.Bounds.Screen.AtAngle(Geometry.Angle).Size;
                Testing.Geometry.Size = size;
            }
        };
    }
    public Shape Testing { get; }
}
