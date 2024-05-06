namespace LootBoot.Epilogue.Game.Entities.Effects;
public abstract class Satellite<TSpawnSatelliteBehavior> : Satellite where TSpawnSatelliteBehavior : SatelliteBehavior, new()
{
    public Satellite(Script parent) : base(parent) => Controller = new SatelliteController<TSpawnSatelliteBehavior>(this);
}
public abstract class Satellite : Effect
{
    public Satellite(Script parent) : base(parent) { }
    public Sprite Sprite { get; protected set; }
    public bool IsGenerated { get; set; }
}
