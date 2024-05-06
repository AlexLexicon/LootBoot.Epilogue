namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts;
public class KilledBehavior : SpacecraftBehavior
{
    private const double EXPLOSION_EVERY_SECONDS = 0.25;
    private const double DEATH_DURATION_SECONDS_MAX = 3;
    private const double DEATH_DURATION_SECONDS_MIN = 1;
    private DateTime? explosionSeconds;
    private DateTime? deathDuration;
    private double deathDurationSeconds;
    protected override void Create()
    {
        base.Create();
        Weapon.IsActivated = false;
        deathDurationSeconds = Rng.ToDouble(DEATH_DURATION_SECONDS_MIN, DEATH_DURATION_SECONDS_MAX);
        Fuselage.Velocity.Set(new Velocity.State(Fuselage.Velocity.Get())
        {

        });
        Fuselage.Rotation.Set(new Rotation.State(Fuselage.Rotation.Get())
        {
            Throttle = Rng.ToDouble(-1, 1),
        });
        Defense.IsAbleToBeHit = false;
        IntegrityIndicator.Sprites.IsVisible = false;
    }
    protected override void Update()
    {
        bool doComplete = Calculations.DateTime.IsSeconds(ref deathDuration, deathDurationSeconds);
        if (doComplete || Calculations.DateTime.IsSeconds(ref explosionSeconds, EXPLOSION_EVERY_SECONDS))
        {
            int totalExplosions = doComplete ? 1 : Rng.ToInteger(1, 3);
            Size fSize = Fuselage.Geometry.Size;
            PVSize fPVSize = fSize.ToPV();
            double fPVWidth = fPVSize.Width;
            Rect fBounds = Fuselage.Geometry.Bounds;
            for (int count = 0; count < totalExplosions; count++)
            {
                double pvWidth = doComplete ? fPVWidth : Rng.ToDouble(fPVWidth / (totalExplosions + 1), fPVWidth);
                Point position = doComplete ? fBounds.ToCenter() : fBounds.Rng().Inside();
                new Explosion(Source.Parent, position, new PVSize(pvWidth));
            }
            if (doComplete)
            {
                if (Source.CanDropCollectables)
                {
                    int totalScrapToSpawn = Generate.Items.Scraps.RandomCount();
                    Scene scene = Source.Parent;
                    Generate.Items.Scraps.Spawn(totalScrapToSpawn, scene, Geometry.Bounds.World, Geometry.Angle);
                    Generate.Items.Modules.Spawn(scene, Geometry.Position);
                }
                Complete();
            }
        }
        base.Update();
    }
}
