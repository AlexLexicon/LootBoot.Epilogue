namespace LootBoot.Epilogue.Game.Behaviors.Spacecrafts;
public class OutsideSpawnBehavior : SpacecraftBehavior
{
    protected override void Create()
    {
        base.Create();
        Geometry.Position = Viewport.Geometry.Bounds.World.Rng().Outside(Geometry.Size);
        Complete();
    }
}
