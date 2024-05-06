namespace LootBoot.Epilogue.Game.Entities.Projectiles;
public sealed class Mine : Projectile
{
    private const double SIZE = 22;
    private readonly Point spawnPosition;
    private readonly Angle angle;
    private readonly double velocity;
    public Mine(Scene parent, GameTeam team, Point spawnPosition, Angle angle, double velocity) : base(parent, team)
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
        Controller = new MineController(this);
    }
    protected override void Create()
    {
        Geometry.Size = new Size(SIZE, SIZE);
        Geometry.Position = spawnPosition;
        Geometry.Angle = angle;
        Velocity.Value = velocity;
        base.Create();
    }
}
