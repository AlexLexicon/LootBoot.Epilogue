namespace LootBoot.Epilogue.Game.Entities.Indicators;
public sealed class PositionIndicator : Indicator
{
    public enum Sizes
    {
        Small,
        Medium,
        Large,
    }
    private static Size Convert(Sizes size) => size switch
    {
        Sizes.Medium => new PVSize(0.025),
        Sizes.Large => new PVSize(0.05),
        _ => new PVSize(0.01),
    };
    public PositionIndicator(Entity tracked, Color color, Sizes size) : this(tracked, color, Convert(size)) { }
    public PositionIndicator(Entity tracked, Color color, Size size) : base(tracked)
    {
        IsDestroyable = true;
        Geometry.Size = size;
        Shape = new Shape(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Indicators,
            Contour = Sprite.Contours.Triangle,
            Color = color,
        };
        Animator = new SpriteAnimator(this)
            {
                Shape,
            };
        Controller = new Controller(this);
    }
    protected override void Create()
    {
        Controller.Start<PositionIndicatorBehavior>();
        base.Create();
    }
    protected override bool CanDestroy() => IsDestroyable;
    public bool IsDestroyable { get; set; }
    public Shape Shape { get; }
    public SpriteAnimator Animator { get; }
}
