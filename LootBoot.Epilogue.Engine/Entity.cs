namespace LootBoot.Epilogue.Engine;
public abstract class Entity : SpriteCollection
{
    public Entity(Script parent) : this(parent, null, GameSequencer.Permanence.Any) { }
    public Entity(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public Entity(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, baseGeometry, permanence)
    {
        Velocity = new Velocity(this, Geometry);
        Rotation = new Rotation(this, Geometry);
    }
    public Velocity Velocity { get; }
    public Rotation Rotation { get; }
    public Controller Controller { get; set; }
}
