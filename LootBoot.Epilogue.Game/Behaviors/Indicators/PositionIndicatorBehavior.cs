namespace LootBoot.Epilogue.Game.Behaviors.Indicators;
public class PositionIndicatorBehavior : Behavior<PositionIndicator>
{
    public PositionIndicatorBehavior()
    {
        FadeInAnimation = new OpacityAnimation(null, 1);
        FadeOutAnimation = new OpacityAnimation(null, 0);
    }
    protected override void Create()
    {
        Tracked = GetParent<Entity>();
        Geometry = GetScript<Geometry>();
        Animator = GetScript<SpriteAnimator>();
        Size viewportSize = Viewport.Geometry.Size;
        Distance = viewportSize.Height / 8;
        Distance = viewportSize.Height - Distance;
        Distance /= 2;
        base.Create();
    }
    protected override void Update()
    {
        if (!Tracked.IsDestroyed)
        {
            Point viewportPosition = Viewport.Geometry.Position;
            //Geometry.Position = viewportPosition;
            Geometry.Angle = viewportPosition.AngleToTarget(Tracked.Geometry.Position);
            Geometry.Position = viewportPosition.AtAngle(Geometry.Angle, Distance);
            if (Tracked.IsOccluded)
            {
                Animator.Animate(FadeInAnimation);
            }
            else
            {
                Animator.Animate(FadeOutAnimation);
            }
        }
        else
        {
            Animator.Animate(FadeOutAnimation);
            bool fading = !Animator.IsComplete;
            Source.IsDestroyable = fading;
            if (!fading)
            {
                Complete();
            }
        }
        base.Update();
    }
    protected double Distance { get; private set; }
    protected OpacityAnimation FadeInAnimation { get; }
    protected OpacityAnimation FadeOutAnimation { get; }
    protected Entity Tracked { get; private set; }
    protected Geometry Geometry { get; private set; }
    protected SpriteAnimator Animator { get; private set; }
}
