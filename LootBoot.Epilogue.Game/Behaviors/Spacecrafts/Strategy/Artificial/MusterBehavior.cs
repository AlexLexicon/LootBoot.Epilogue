namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts.Strategy.Artificial;
public class MusterBehavior : ArtificialStrategyBehavior
{
    private const double DURATION_SECONDS_MAX = 5;
    private const double DURATION_SECONDS_MIN = 1;
    private DateTime? durationTime;
    protected override void Create()
    {
        base.Create();
        Velocity.MoveTo = null;
        Velocity.Throttle = 0.20;
        Rotation.Throttle = 0.5;
        Duration = Rng.ToDouble(DURATION_SECONDS_MAX, DURATION_SECONDS_MIN);
    }
    protected override void Update()
    {
        Rotation.RotateTo = PlayerPosition;
        if (Calculations.DateTime.IsSeconds(ref durationTime, Duration))
        {
            Complete();
        }
        base.Update();
    }
    private double Duration { get; set; }
}
