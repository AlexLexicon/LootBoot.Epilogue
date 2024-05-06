namespace LootBoot.Epilogue.Game.Entities.Indicators;
public class AlertIndicator : Indicator
{
    public AlertIndicator(Fuselage parent, Color color) : base(parent, parent?.Geometry)
    {
        Sprite = new Text(this, Geometry)
        {
            Color = color,
            TypeFace = "Tw Cen MT",
            ZIndex = (int)Configurations.Sprites.ZIndex.Text,
        };
        double gapSize = new PVSize(0.002).Size.Width;
        Size size = Geometry.Size;
        double y = -(size.Height / 4 * 3 + gapSize);
        Sprite.Geometry.Position.IsRotatable = false;
        Sprite.Geometry.Position = new Point(0, y);
        Sprite.Geometry.Angle.IsDerivable = false;
        Sprite.Geometry.Angle = 0;
        Animator = new SpriteAnimator(this)
            {
                Sprite,
            };
        Controller = new Controller(this);
    }
    protected override void Create()
    {
        Controller.Start<AlertIndicatorBehavior>();
        base.Create();
    }
    public void Show(string message)
    {
        Sprite.Value = message;
        IsShow = true;
    }
    public new Fuselage Parent => (Fuselage)base.Parent;
    public bool IsShow { get; set; }
    public Text Sprite { get; }
    public SpriteAnimator Animator { get; }
}
