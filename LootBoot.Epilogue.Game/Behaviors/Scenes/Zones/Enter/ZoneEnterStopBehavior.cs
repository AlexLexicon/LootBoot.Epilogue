namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones.Enter;
public class ZoneEnterStopBehavior : ZoneEnterBehavior
{
    protected const double COMPLETE_SECONDS = 2;
    private DateTime? completeTime;
    protected override void Create()
    {
        base.Create();
        Viewport.Watched.Set(Geometry);
        FuselageAnimator.Animate(new SizeScaleAnimation(2, 1, 0.015));
        WeaponAnimator.Animate(new SizeScaleAnimation(2, 1, 0.015));
    }
    protected override void Update()
    {
        if (Calculations.DateTime.IsSeconds(ref completeTime, COMPLETE_SECONDS))
        {
            Complete();
        }
        base.Update();
    }
    public override void Complete()
    {
        Source.Fuselage.Sprites.IsAutomaticAnimation = true;
        StarStreak.IsFTL = false;
        Fuselage.ResetTranslations();
        base.Complete();
    }
    protected SpriteAnimator FuselageAnimator => Fuselage.Animator;
    protected SpriteAnimator WeaponAnimator => Weapon.Animator;
}
