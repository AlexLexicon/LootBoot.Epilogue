namespace LootBoot.Epilogue.Game.Entities.Indicators;
public sealed class IntegrityIndicator : Indicator
{
    public IntegrityIndicator(Fuselage parent, Color color) : base(parent, parent?.Geometry)
    {
        Sprites = new IntegritySprite(this, color);
        Animator = new SpriteAnimator(this)
        {
            Sprites,
        };
        Controller = new Controller(this);
    }
    protected override void Create()
    {
        Controller.Start<IntegrityIndicatorBehavior>();
        base.Create();
    }
    public new Fuselage Parent => (Fuselage)base.Parent;
    public bool IsExtendingLifetime { get; set; }
    public IntegritySprite Sprites { get; }
    public SpriteAnimator Animator { get; }
}
