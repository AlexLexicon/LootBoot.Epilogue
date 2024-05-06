namespace LootBoot.Epilogue.Game.Sprites;
public abstract class ItemSprite : Texture
{
    public ItemSprite(Item item) : base(item, item.Geometry)
    {
        ZIndex = (int)Configurations.Sprites.ZIndex.Items;
        AnimationIndex = GetAnimationIndex(item.Rarity);
    }
    protected abstract int GetAnimationIndex(Rarity rarity);
    public new Item Parent => (Item)base.Parent;
}
