namespace LootBoot.Epilogue.Game.Sprites.Weapons;
public class ArcSprite : WeaponSprites
{
    public ArcSprite(Weapon parent) : base(parent) { }
    protected override Texture GetNewSprite(Geometry baseGeometry) => new Texture(this, baseGeometry)
    {
        ZIndex = (int)Configurations.Sprites.ZIndex.Weapons,
        ImageFilePaths = new ObservableCollection<string>
        {
        }
    };
}
