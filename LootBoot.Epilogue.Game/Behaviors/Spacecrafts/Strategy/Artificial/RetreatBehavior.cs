namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts.Strategy.Artificial;
public class RetreatBehavior : ArtificialStrategyBehavior
{
    private const double COMPLETE_RADIUS_MAX = 800;
    private const double COMPLETE_RADIUS_MIN = 750;
    private const double RETREAT_ATTEMPTS_MAXIMUM = 5;
    private const double RETREAT_ATTEMPTS_SECONDS = 5;
    private const double DODGE_RADIUS = 100;
    private const int DODGE_ANGLE_MAX = 45;
    private DateTime? retreatTime;
    protected override void Create()
    {
        base.Create();
        Velocity.MoveTo = null;
        Rotation.RotateTo = null;
        CompleteRadius = Rng.ToDouble(COMPLETE_RADIUS_MIN, COMPLETE_RADIUS_MAX);
    }
    protected override void Update()
    {
        Velocity.Throttle = (double)(1 / RETREAT_ATTEMPTS_MAXIMUM) * (RetreatAttempts + 1);
        if (Rotation.AngleTo is not null)
        {
            Rotation.Throttle = 1;
            if (Geometry.Angle == Rotation.AngleTo)
            {
                Rotation.AngleTo = null;
            }
        }
        else
        {
            Rotation.Throttle = 0;
        }
        if (Calculations.DateTime.IsSeconds(ref retreatTime, RETREAT_ATTEMPTS_SECONDS))
        {
            if (PlayerDistance > CompleteRadius)
            {
                Complete();
            }
            else
            {
                if (PlayerDistance < DODGE_RADIUS)
                {
                    Rotation.AngleTo = Geometry.Angle + Rng.ToInteger(-DODGE_ANGLE_MAX, DODGE_ANGLE_MAX);
                }
                RetreatAttempts++;
                retreatTime = null;
            }
        }
        base.Update();
    }
    private int RetreatAttempts { get; set; }
    private double CompleteRadius { get; set; }
}
