namespace LootBoot.Epilogue.Game.Entities.Items;
public sealed class Scrap : Item
{
    public Scrap(Collectable collectable, ItemStats stats, Point? spawnPosition, Angle? spawnAngle) : base(collectable, stats, Limits.Items.Standard, spawnPosition, spawnAngle) => Sprite = new ScrapSprite(this);
}
