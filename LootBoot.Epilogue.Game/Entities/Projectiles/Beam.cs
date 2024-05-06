namespace LootBoot.Epilogue.Game.Entities.Projectiles;
public sealed class Beam : Projectile
{
    private const double WIDTH = 4;
    private readonly double velocity;
    public Beam(Scene parent, GameTeam team, Geometry origin, double velocity) : base(parent, team)
    {
        this.velocity = velocity;
        Origin = origin;
        Sprite = new Shape(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Projectiles,
            Contour = Sprite.Contours.Rectangle,
            StrokeThickness = 0,
        };
        Controller = new BeamController(this);
    }
    protected override void Create()
    {
        Geometry.Size = new Size(WIDTH, 0);
        Velocity.Value = velocity;
        base.Create();
    }
    public Geometry Origin { get; }
}
