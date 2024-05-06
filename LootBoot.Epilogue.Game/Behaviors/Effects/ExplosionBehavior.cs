namespace LootBoot.Epilogue.Game.Behaviors.Effects;
public class ExplosionBehavior : Behavior<Explosion>
{
    private DateTime? startTime;
    protected override void Create()
    {
        int? animationIndexMaximum = Source.Sprite.AnimationIndexMaximum;
        int totalAnimationIndices = (animationIndexMaximum is null ? 0 : animationIndexMaximum.Value) + 1;
        double secondsPerIndex = Source.Sprite.AnimationSeconds;
        DurationSeconds = secondsPerIndex * totalAnimationIndices;
        base.Create();
    }
    protected override void Update()
    {
        if (Calculations.DateTime.IsSeconds(ref startTime, DurationSeconds))
        {
            Complete();
        }
        base.Update();
    }
    protected double DurationSeconds { get; set; }
}
