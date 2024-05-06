namespace LootBoot.Epilogue.Game.Entities.Projectiles;
public sealed class Flame : Projectile
{
    private readonly Point spawnPosition;
    private readonly Angle angle;
    private readonly double velocity;
    public Flame(Scene parent, GameTeam team, Point spawnPosition, Angle angle, double velocity) : base(parent, team)
    {
        this.spawnPosition = spawnPosition;
        this.angle = angle;
        this.velocity = velocity;
        Sprite = new Texture(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Projectiles,
            ImageFilePaths = new()
            {

            },
        };
        Controller = new FlameController(this);
    }
    protected override void Create()
    {
        Geometry.Position = spawnPosition;
        Geometry.Angle = angle;
        Velocity.Value = velocity;
        base.Create();
    }
}
