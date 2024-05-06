namespace LootBoot.Epilogue.Game.Behaviors.Indicators;
public class IntegrityIndicatorBehavior : Behavior<IntegrityIndicator>
{
    public IntegrityIndicatorBehavior() => FadeOutAnimation = new OpacityAnimation(1, 0);
    protected override void Create()
    {
        Fuselage = GetParent<Fuselage>();
        Geometry = GetScript<Geometry>();
        Animator = GetScript<SpriteAnimator>();
        Player.Actor.Spacecraft.OnWeaponChange += () => IntegrityChange(true);
        Fuselage.Defense.OnHit += () => IntegrityChange(false);
        IntegrityChange(true);
        base.Create();
    }
    protected override void Update()
    {
        if (Fuselage.IsDestroyed)
        {
            Animator.Animate(FadeOutAnimation);
            bool fading = !Animator.IsComplete;
            Source.IsExtendingLifetime = fading;
            if (!fading)
            {
                Complete();
            }
        }
        base.Update();
    }
    protected void IntegrityChange(bool weaponChanged)
    {
        int totalMarks = HitsToKill.Get(Player.Actor.Stats.Weapon, Fuselage.Attributes.Stats);
        int remainingMarks = totalMarks - Fuselage.Defense.Hits;
        if (weaponChanged && remainingMarks <= 0)
        {
            remainingMarks = 1;
        }
        Source.Sprites.SetMarks(remainingMarks, totalMarks);
    }
    protected OpacityAnimation FadeOutAnimation { get; }
    protected Fuselage Fuselage { get; private set; }
    protected Geometry Geometry { get; private set; }
    protected SpriteAnimator Animator { get; private set; }
}
