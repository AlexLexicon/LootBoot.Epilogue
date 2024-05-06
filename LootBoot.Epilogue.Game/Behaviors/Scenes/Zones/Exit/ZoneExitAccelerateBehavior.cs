namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones.Exit;
public class ZoneExitAccelerateBehavior : ZoneExitBehavior
{
    protected const double SPACECRAFT_VELOCITY = 2000;
    protected const double GEOMETRY_ANGLE = 45;
    private const double VIEWPORT_VELOCITY = 500;
    protected override void Create()
    {
        base.Create();
        Geometry.Angle = GEOMETRY_ANGLE;
        Velocity.Set(new Velocity.State
        {
            Maximum = SPACECRAFT_VELOCITY,
            Minimum = 0,
            Acceleration = 100,
            Momentum = 100,
            Throttle = 1,
        });
        Source.Fuselage.Sprites.IsAutomaticAnimation = false;
        Source.Fuselage.Sprites.EngineAnimation = FuselageSprites.EngineAnimations.Thrust;
        Weapon.IsActivated = false;
        Viewport.Watched.Set(null);
    }
    protected override void Update()
    {
        Viewport.Geometry.MoveAtAngle(VIEWPORT_VELOCITY);
        if (Source.IsOccluded)
        {
            Complete();
        }
        base.Update();
    }
    protected SpriteAnimator FuselageAnimator => Fuselage.Animator;
    protected SpriteAnimator WeaponAnimator => Weapon.Animator;
}
