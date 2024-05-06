namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts.Strategy;
public class ArtificialStrategyBehavior : SpacecraftBehavior
{
    private const double ERROR_RADIUS_RANGE = 2500;
    private const double ERROR_RESET_DISTANCE = 1000;
    protected override void Create()
    {
        base.Create();
        Defense.OnHit += () =>
        {
            int totalHitsToKill = HitsToKill.Get(Player.Actor.Stats.Weapon, Fuselage.Attributes.Stats);
            if (Defense.Hits >= totalHitsToKill)
            {
                Defense.Kill();
            }
        };
    }
    protected override void Update()
    {
        if (PlayerDistance > ERROR_RADIUS_RANGE)
        {
            Geometry.RotateToPosition(PlayerPosition);
            Geometry.MoveAtAngle(ERROR_RESET_DISTANCE);
        }
        Weapon.IsActivated = IsWeaponActivated;
        base.Update();
    }
    protected Point PlayerPosition => Player.Geometry.Position ?? new Point();
    protected double PlayerDistance => Player.Geometry.DistanceTo(Geometry) ?? 0;
    protected bool IsWeaponActivated { get; set; }
}
