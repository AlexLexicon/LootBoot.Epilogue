namespace LootBoot.Epilogue.Game.Entities;
public abstract class Item : Entity
{
    private readonly Point? _spawnPosition;
    private readonly Angle? _spawnAngle;
    protected Item(Collectable collectable, ItemStats stats, ItemLimits limits, Point? spawnPosition, Angle? spawnAngle) : base(collectable)
    {
        _spawnPosition = spawnPosition;
        _spawnAngle = spawnAngle;
        Geometry.Size = new PVSize(0.005);
        ItemController controller = new(this);
        controller.OnPickedUp += () => Parent.Collect();
        Controller = controller;
        Attributes = new ItemAttributes(this, stats, limits);
        Rarity = stats.Rarity;
    }
    protected override void Create()
    {
        Point spawnPosition;
        if (_spawnPosition is not null)
        {
            spawnPosition = _spawnPosition.Value;
        }
        else
        {
            spawnPosition = Viewport.Geometry.Bounds.World.Rng().Inside();
        }
        Geometry.Position = spawnPosition;
        Angle spawnAngle;
        if (_spawnAngle is not null)
        {
            spawnAngle = _spawnAngle.Value;
        }
        else
        {
            spawnAngle = Rng.Geomerty.Angle();
        }
        Geometry.Angle = spawnAngle;
        if (_spawnPosition is not null && _spawnAngle is not null)
        {
            Velocity.Set(Attributes.Velocity);
            Velocity.Value = Rng.ToDouble(Attributes.Velocity.Maximum / 2, Attributes.Velocity.Maximum);
            Velocity.Momentum = Rng.ToDouble(Attributes.Velocity.Momentum / 2, Attributes.Velocity.Momentum);
        }
        base.Create();
    }
    public new Collectable Parent => (Collectable)base.Parent;
    public ItemAttributes Attributes { get; }
    public ItemSprite Sprite { get; protected set; }
    public Rarity Rarity { get; }
}
