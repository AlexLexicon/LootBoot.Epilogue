namespace LootBoot.Epilogue.Game.Controllers;
public class SatelliteController<TSpawnSatelliteBehavior> : Controller where TSpawnSatelliteBehavior : SatelliteBehavior, new()
{
    public SatelliteController(Script parent) : base(parent) => Sequence<TSpawnSatelliteBehavior>().Complete().Start<TSpawnSatelliteBehavior>();
    protected override void Create()
    {
        Start<TSpawnSatelliteBehavior>();
        base.Create();
    }
}
