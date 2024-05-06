namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones.Exit;
public class ZoneExitRotateBehavior : ZoneExitBehavior
{
    protected override void Create()
    {
        base.Create();
        Rotation.Set(new Rotation.State
        {
            Maximum = 150,
            Minimum = 50,
            Acceleration = 30,
        });
        Velocity.Set(new Velocity.State
        {
            Maximum = 800,
            Minimum = -300,
            Acceleration = 25,
            Momentum = 25,
            Throttle = 0,
            Value = Velocity.Value,
        });
        Weapon.IsActivated = false;
        Rotation.AngleTo = 45;
        FuselageAnimator.Animate(new SizeScaleAnimation(1, 2, 0.025));
        WeaponAnimator.Animate(new SizeScaleAnimation(1, 2, 0.025));
    }
    protected override void Update()
    {
        if (Velocity.Value == 0 && Rotation.IsRotateReached)
        {
            Complete();
        }
        base.Update();
    }
    protected SpriteAnimator FuselageAnimator => Fuselage.Animator;
    protected SpriteAnimator WeaponAnimator => Weapon.Animator;
}
