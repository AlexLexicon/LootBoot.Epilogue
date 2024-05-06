namespace LootBoot.Epilogue.Game.Behaviors.Indicators;
public class AlertIndicatorBehavior : Behavior<AlertIndicator>
{
    private const double SHOW_DURATION_SECONDS = 1;
    private DateTime? _showSeconds;
    public AlertIndicatorBehavior()
    {
        FadeInAnimation = new OpacityAnimation(null, 1, 0.1);
        FadeOutAnimation = new OpacityAnimation(null, 0, 0.1);
    }
    protected override void Create()
    {
        Animator = GetScript<SpriteAnimator>();
        base.Create();
    }
    protected override void Update()
    {
        if (Source.IsShow)
        {
            Animator.Animate(FadeInAnimation);
            if (Calculations.DateTime.IsSeconds(ref _showSeconds, SHOW_DURATION_SECONDS))
            {
                Source.IsShow = false;
            }
        }
        else
        {
            _showSeconds = null;
            Animator.Animate(FadeOutAnimation);
        }
        base.Update();
    }
    protected OpacityAnimation FadeInAnimation { get; }
    protected OpacityAnimation FadeOutAnimation { get; }
    protected SpriteAnimator Animator { get; private set; }
}
