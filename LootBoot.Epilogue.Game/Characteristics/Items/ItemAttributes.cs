namespace LootBoot.Epilogue.Game.Characteristics.Items;
public class ItemAttributes : Attributes<ItemLimits, ItemStats>
{
    public ItemAttributes(Script parent, ItemStats stats, ItemLimits limits) : base(parent, stats, limits) => Velocity = new VelocityAttributes(this, stats.Velocity, limits.Velocity);
    public VelocityAttributes Velocity { get; }
}
