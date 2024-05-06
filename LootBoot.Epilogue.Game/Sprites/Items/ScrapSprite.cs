namespace LootBoot.Epilogue.Game.Sprites.Items;
public class ScrapSprite : ItemSprite
{
    public ScrapSprite(Item item) : base(item) => ImageFilePaths = new ObservableCollection<string>
    {
        "sprites.items.scrap.13.00-00-00.png",
    };
    protected override int GetAnimationIndex(Rarity rarity) => rarity switch
    {
        _ => 0,
    };
}
