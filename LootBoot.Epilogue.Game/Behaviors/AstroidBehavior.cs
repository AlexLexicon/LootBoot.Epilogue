namespace LootBoot.Epilogue.Game.Behaviors;
public class AstroidBehavior : Behavior<Astroid>
{
    private const double DESPAWN_SECONDS = 5;
    private const double VELOCITY_MIN = 50;
    private const double VELOCITY_MAX = 300;
    private const double ANGLE_CHANGE_MIN = 0.25;
    private const double ANGLE_CHANGE_MAX = 1;
    private DateTime? despawnTime;
    protected override void Create()
    {
        Geometry = GetScript<Geometry>();
        Velocity = GetScript<Velocity>();
        Sprite = GetScript<Texture>();
        Defense = GetScript<Defense>();
        Velocity.Value = Rng.ToDouble(VELOCITY_MIN, VELOCITY_MAX);
        AngleChange = Rng.ToDouble(ANGLE_CHANGE_MIN, ANGLE_CHANGE_MAX);
        AngleChange = Rng.Negate(AngleChange.Degrees);
        Sprite.Geometry.Angle.IsDerivable = false;
        HitsToKill = Source.Size switch
        {
            Astroid.Sizes.Large => 4,
            Astroid.Sizes.Medium => 3,
            _ => 2,
        };
        base.Create();
    }
    protected override void Update()
    {
        Angle angle = Sprite.Geometry.Angle;
        Sprite.Geometry.Angle = angle + AngleChange;
        if (Defense.Hits >= HitsToKill)
        {
            Defense.Kill();
        }
        bool complete = Defense.IsKilled;
        if (!complete)
        {
            if (Source.IsOccluded)
            {
                if (Calculations.DateTime.IsSeconds(ref despawnTime, DESPAWN_SECONDS))
                {
                    complete = true;
                }
            }
            else
            {
                despawnTime = null;
            }
        }
        if (complete)
        {
            Complete();
        }
        base.Update();
    }
    protected Geometry Geometry { get; private set; }
    protected Velocity Velocity { get; private set; }
    protected Texture Sprite { get; private set; }
    protected Defense Defense { get; private set; }
    protected Angle AngleChange { get; set; }
    protected int HitsToKill { get; set; }
}
