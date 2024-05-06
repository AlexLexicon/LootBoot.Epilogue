namespace LootBoot.Epilogue.Game.Behaviors;
public abstract class ProjectileBehavior : ProjectileBehavior<Script> { }
public abstract class ProjectileBehavior<TSourceScript> : Behavior<TSourceScript> where TSourceScript : Script
{
    private const double DESPAWN_DISTANCE = 2000;
    protected override void Create()
    {
        Geometry = GetScript<Geometry>();
        Offense = GetScript<Offense>();
        base.Create();
    }
    protected override void Update()
    {
        double? distance = Player.Geometry.DistanceTo(Geometry);
        if (distance > DESPAWN_DISTANCE || Offense.Attack())
        {
            Complete();
        }
        base.Update();
    }
    protected Geometry Geometry { get; private set; }
    protected Offense Offense { get; private set; }
}
