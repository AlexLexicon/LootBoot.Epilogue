namespace LootBoot.Epilogue.Game.Behaviors.Weapons;
public class CannonBehavior : WeaponBehavior<Cannon>
{
    private int barrelIndex;
    protected override void Activate()
    {
        int totalHardpoints = Source.Hardpoints.Length;
        if (totalHardpoints > 0)
        {
            if (totalHardpoints > 1 && Source.Attributes.Stats.Traits.Contains(Balance.Traits.Weapons.MULTISHOT))
            {
                foreach (Hardpoint hardpoint in Source.Hardpoints)
                {
                    Shoot(hardpoint);
                }
            }
            else
            {
                if (barrelIndex >= totalHardpoints)
                {
                    barrelIndex = 0;
                }
                Shoot(Source.Hardpoints[barrelIndex]);
                barrelIndex++;
            }
        }
    }
    protected void Shoot(Hardpoint hardpoint)
    {
        //double spacecraftVelocity = Source.Parent.Fuselage.Velocity.Value;
        double velocity = Source.Attributes.Velocity;//spacecraftVelocity + Source.Attributes.Velocity;
        Point spawnPosition = hardpoint.GetBarrel(Geometry);
        new Pulse(Scene, Source.Team, spawnPosition, Geometry.Angle, velocity);
    }
}
