namespace LootBoot.Epilogue.Game.Entities.Effects.Satellites;
public sealed class StarPoint : Satellite<StarPointBehavior>
{
    public StarPoint(Script parent) : base(parent) => Sprite = new Shape(this, Geometry)
    {
        ZIndex = (int)Configurations.Sprites.ZIndex.Satellites,
        Contour = Sprite.Contours.Ellipse,
    };
}
