namespace LootBoot.Epilogue.Game.Sprites.Items;
public class ModuleSprite : ItemSprite
{
    public ModuleSprite(Item item) : base(item) => ImageFilePaths = new ObservableCollection<string>
    {
        "sprites.items.module.13.00-00-00.png",
        "sprites.items.module.13.01-00-00.png",
    };
    protected override int GetAnimationIndex(Rarity rarity) => rarity switch
    {
        Rarity.Remarkable => 1,
        _ => 0,
    };
}
