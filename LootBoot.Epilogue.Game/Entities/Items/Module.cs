namespace LootBoot.Epilogue.Game.Entities.Items;
public sealed class Module : Item
{
    public Module(Collectable collectable, ItemStats stats, Point? spawnPosition, Angle? spawnAngle) : base(collectable, stats, Limits.Items.Standard, spawnPosition, spawnAngle)
    {
        Sprite = new ModuleSprite(this);
        PositionIndicator = new PositionIndicator(this, Rarity.GetColor(), PositionIndicator.Sizes.Small);
        Geometry.Size = new PVSize(0.01);
    }
    public PositionIndicator PositionIndicator { get; }
}
