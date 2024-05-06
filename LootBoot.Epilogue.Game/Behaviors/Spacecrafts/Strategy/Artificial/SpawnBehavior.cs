namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts.Strategy.Artificial;
public class SpawnBehavior : OutsideSpawnBehavior
{
    protected override void Create()
    {
        Geometry.Angle = Player.Geometry.AngleTo(Geometry);
        base.Create();
    }
}
