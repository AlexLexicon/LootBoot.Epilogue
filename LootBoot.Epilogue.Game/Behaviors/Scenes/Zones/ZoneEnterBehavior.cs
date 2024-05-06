namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones;
public abstract class ZoneEnterBehavior : SpacecraftBehavior
{
    protected const double SPACECRAFT_VELOCITY = 2000;
    protected const double GEOMETRY_ANGLE = 45;
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
        Rotation.Set(new());
        StarStreak.IsFTL = true;
        StarStreak.Conductor = Source.Fuselage;
    }
}
