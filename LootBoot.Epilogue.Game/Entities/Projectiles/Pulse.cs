namespace LootBoot.Epilogue.Game.Entities.Projectiles;
public sealed class Pulse : Projectile
{
    private const double SIZE = 0.004;
    private readonly Point spawnPosition;
    private readonly Angle angle;
    private readonly double velocity;
    public Pulse(Scene parent, GameTeam team, Point spawnPosition, Angle angle, double velocity) : base(parent, team)
    {
        this.spawnPosition = spawnPosition;
        this.angle = angle;
        this.velocity = velocity;
        Sprite = new Shape(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Projectiles,
            Contour = Sprite.Contours.Ellipse,
            StrokeThickness = 0,
            Color = Offense.Team.Color,
        };
        Controller = new PulseController(this);
    }
    protected override void Create()
    {
        Geometry.Size = new PVSize(SIZE);
        Geometry.Position = spawnPosition;
        Geometry.Angle = angle;
        Velocity.Value = velocity;
        base.Create();
    }
    protected override void Attack()
    {
        new Explosion(Parent, Geometry.Position, new PVSize(SIZE * 8));
        base.Attack();
    }
}
