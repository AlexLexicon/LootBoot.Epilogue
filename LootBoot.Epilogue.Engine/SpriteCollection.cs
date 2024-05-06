namespace LootBoot.Epilogue.Engine;
public abstract class SpriteCollection : Script, IEnumerable<Sprite>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Sprite> GetEnumerator() => Sprites.GetEnumerator();
    public SpriteCollection(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public SpriteCollection(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, permanence) => Geometry = new Geometry(this, baseGeometry);
    public virtual bool IsVisible
    {
        get
        {
            HashSet<Sprite> sprites = Sprites;
            if (sprites is not null && sprites.Count > 0)
            {
                if (sprites.Any(sprite => sprite.IsVisible == true))
                {
                    return true;
                }
            }
            HashSet<SpriteCollection> spriteCollections = GetScripts<SpriteCollection>();
            if (spriteCollections is not null && spriteCollections.Count > 0)
            {
                foreach (SpriteCollection spriteCollection in spriteCollections)
                {
                    if (spriteCollection != this && spriteCollection.IsVisible)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public virtual bool IsOccluded
    {
        get
        {
            return Viewport.IsOccluded(Geometry);
            //HashSet<Sprite> sprites = Sprites;
            //if (sprites is not null && sprites.Count > 0)
            //{
            //    if (sprites.Any(sprite => sprite.IsOccluded == false))
            //    {
            //        return false;
            //    }
            //}
            //HashSet<SpriteCollection> spriteCollections = GetScripts<SpriteCollection>();
            //if (spriteCollections is not null && spriteCollections.Count > 0)
            //{
            //    foreach (SpriteCollection spriteCollection in spriteCollections)
            //    {
            //        if (spriteCollection != this)
            //        {
            //            if (!spriteCollection.IsOccluded)
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}
            //return true;
        }
    }
    public Geometry Geometry { get; }
    private HashSet<Sprite> Sprites => GetScripts<Sprite>();
}
