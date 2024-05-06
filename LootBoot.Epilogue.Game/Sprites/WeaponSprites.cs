namespace LootBoot.Epilogue.Game.Sprites;
public abstract class WeaponSprites : SpriteCollection
{
    public event Action ReGenerated;
    private Hardpoint[] _savedHardpoints;
    private double? _previousSizeScale;
    public WeaponSprites(Weapon parent) : base(parent, parent.Geometry) { }
    public void SetSizeScale(double sizeScale)
    {
        SizeScale = sizeScale;
        UpdateSprites(_savedHardpoints);
    }
    public virtual void UpdateSprites(Hardpoint[] hardpoints)
    {
        bool isRegenerationRequired = hardpoints is null || _savedHardpoints is null || hardpoints.Length != _savedHardpoints.Length;
        HashSet<Texture> textures = GetScripts<Texture>();
        if (isRegenerationRequired)
        {
            ReGenerateSprites(textures, hardpoints);
        }
        else
        {
            int index = 0;
            foreach (Texture texture in textures)
            {
                if (SizeScale is not null && SizeScale != _previousSizeScale)
                {
                    texture.SizeScale = SizeScale.Value;
                }
                double sizeScale = texture.SizeScale;
                texture.Geometry.Position = hardpoints[index].GetCenterValue(sizeScale);
                index++;
            }
        }
        _previousSizeScale = SizeScale;
        _savedHardpoints = hardpoints;
    }
    protected virtual void ReGenerateSprites(HashSet<Texture> textures, Hardpoint[] hardpoints)
    {
        foreach (Texture texture in textures)
        {
            texture.QueueDestroy();
        }
        if (hardpoints is not null && hardpoints.Length > 0)
        {
            Texture original = GetNewSprite(Geometry);
            if (SizeScale is not null)
            {
                original.SizeScale = SizeScale.Value;
            }
            if (original is not null)
            {
                original.Geometry.Position = hardpoints[0].GetCenterValue(SizeScale);
                //Sprites.Add(original);
                for (int count = 1; count < hardpoints.Length; count++)
                {
                    Texture clone = (Texture)original.Clone();
                    clone.Geometry.Position = hardpoints[count].GetCenterValue(SizeScale);
                    //Sprites.Add(clone);
                }
            }
        }
        ReGenerated?.Invoke();
    }
    protected abstract Texture GetNewSprite(Geometry baseGeometry);
    public new Weapon Parent => (Weapon)base.Parent;
    private double? SizeScale { get; set; }
}
