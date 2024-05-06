namespace LootBoot.Epilogue.Game.Entities.Effects;
public class Explosion : Effect
{
    private readonly Point position;
    private readonly PVSize size;
    public Explosion(Script parent, Point position, PVSize size) : base(parent)
    {
        this.position = position;
        this.size = size;
        Sprite = new Texture(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Explosions,
            ImageFilePaths = new()
            {
                "sprites.effects.explosions.19.00-00-00.png",
                "sprites.effects.explosions.19.00-01-00.png",
                "sprites.effects.explosions.19.00-02-00.png",
                "sprites.effects.explosions.19.00-03-00.png",
                "sprites.effects.explosions.19.00-04-00.png",
                "sprites.effects.explosions.19.00-05-00.png",
                "sprites.effects.explosions.19.00-06-00.png",
            },
            AnimationRepeatBehavior = SpriteAnimation.RepeatBehaviors.None,
            AnimationSeconds = 0.05,
            AnimationIndexMinimum = 0,
            AnimationIndexMaximum = 6,
        };
        Controller = new Controller(this);
        Controller.When<ExplosionBehavior>().IsCompleted().Run(QueueDestroy);
    }
    protected override void Create()
    {
        Geometry.Position = position;
        Geometry.Size = size;
        Controller.Start<ExplosionBehavior>();
        base.Create();
    }
    public Texture Sprite { get; }
}
