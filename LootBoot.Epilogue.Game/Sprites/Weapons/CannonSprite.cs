namespace LootBoot.Epilogue.Game.Sprites.Weapons;
public class CannonSprite : WeaponSprites
{
    public CannonSprite(Weapon parent) : base(parent) { }
    protected override Texture GetNewSprite(Geometry baseGeometry) => new Texture(this, baseGeometry)
    {
        ZIndex = (int)Configurations.Sprites.ZIndex.Weapons,
        ImageFilePaths = new ObservableCollection<string>
        {
            "sprites.weapons.cannon.5-11.00.00.00.png"
        }
    };
}
