namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts.Strategy.Artificial;
public class ChaseBehavior : ArtificialStrategyBehavior
{
    private const double WEAPON_RADIUS_MAX = 800;
    private const double WEAPON_RADIUS_MIN = 750;
    private const double COMPLETE_RADIUS_MAX = 75;
    private const double COMPLETE_RADIUS_MIN = 50;
    private const double RETARGET_SECONDS = 2;
    private DateTime? retargetTime;
    protected override void Create()
    {
        base.Create();
        Velocity.MoveTo = null;
        Rotation.Throttle = 1;
        WeaponRadius = Rng.ToDouble(WEAPON_RADIUS_MAX, WEAPON_RADIUS_MIN);
        CompleteRadius = Rng.ToDouble(COMPLETE_RADIUS_MAX, COMPLETE_RADIUS_MIN);
    }
    protected override void Update()
    {
        if (Calculations.DateTime.IsSeconds(ref retargetTime, RETARGET_SECONDS))
        {
            Target = PlayerPosition;
        }
        Rotation.RotateTo = Target;
        Velocity.Throttle = 1;
        IsWeaponActivated = PlayerDistance < WeaponRadius;
        if (PlayerDistance < CompleteRadius)
        {
            Complete();
        }
        else
        {
            Angle angle = Geometry.Angle;
            double degrees = Math.Round(angle.Degrees);
            Angle angleToTarget = Geometry.AngleToTarget(Target);
            double degreesToTarget = Math.Round(angleToTarget.Degrees);
            if (degrees > degreesToTarget + 45 || degrees < degreesToTarget - 45)
            {
                Velocity.Throttle = 0.3;
            }
        }
        base.Update();
    }
    private Point Target { get; set; }
    private double WeaponRadius { get; set; }
    private double CompleteRadius { get; set; }
}
