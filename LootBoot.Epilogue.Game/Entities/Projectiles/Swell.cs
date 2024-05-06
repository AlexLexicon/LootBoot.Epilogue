namespace LootBoot.Epilogue.Game.Entities.Projectiles;
public sealed class Swell : Projectile
{
    private readonly Point spawnPosition;
    private readonly Angle angle;
    private readonly double velocity;
    public Swell(Scene parent, GameTeam team, Point spawnPosition, Angle angle, double velocity) : base(parent, team)
    {
        this.spawnPosition = spawnPosition;
        this.angle = angle;
        this.velocity = velocity;
        Sprite = new Shape(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Projectiles,
            Contour = Sprite.Contours.Triangle,
            StrokeThickness = 0,
        };
        Controller = new SwellController(this);
    }
    protected override void Create()
    {
        Geometry.Position = spawnPosition;
        Geometry.Angle = angle;
        Velocity.Value = velocity;
        base.Create();
    }
}
