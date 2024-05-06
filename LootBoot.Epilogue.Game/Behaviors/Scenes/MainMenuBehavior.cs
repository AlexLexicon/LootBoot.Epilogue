namespace LootBoot.Epilogue.Game.Behaviors.Scenes;
public abstract class MainMenuBehavior : SpacecraftBehavior
{
    protected const double GEOMETRY_ANGLE = 45;
    protected const double SPACECRAFT_VELOCITY = 2000;
    protected const double SPACECRAFT_SPRITE_X = 0.5;
    protected const double SPACECRAFT_SPRITE_Y = -0.3;
    protected const double SPACECRAFT_SIZE_SCALE = 5;
    protected override void Create()
    {
        base.Create();
        Geometry.Angle = GEOMETRY_ANGLE;
        Velocity.Set(new()
        {
            Value = SPACECRAFT_VELOCITY,
        });

        Source.Fuselage.Sprites.IsAutomaticAnimation = false;
        Source.Fuselage.Sprites.EngineAnimation = FuselageSprites.EngineAnimations.Thrust;

        Point position = new PVPoint(SPACECRAFT_SPRITE_X, SPACECRAFT_SPRITE_Y);
        Source.Fuselage.Sprites.Geometry.Position.IsRotatable = false;
        Source.Fuselage.Sprites.Geometry.Position = position;

        Source.Weapon.Sprites.Geometry.Position.IsRotatable = false;
        Source.Weapon.Sprites.Geometry.Position = position;
        //foreach (Sprite sprite in Source.Fuselage.Sprites)
        //{
        //    sprite.Geometry.Position.IsRotatable = false;
        //    sprite.Geometry.Position = position;
        //}
        //foreach (Sprite sprite in Source.Weapon.Sprites)
        //{
        //    sprite.Geometry.Position.IsRotatable = false;
        //    sprite.Geometry.Position = position;
        //}

        Source.SetScale(SPACECRAFT_SIZE_SCALE);

        StarStreak.Conductor = Source.Fuselage;
    }
    protected override void Destroy()
    {
        StarStreak.IsFTL = false;
        base.Destroy();
    }
}
