namespace LootBoot.Epilogue.Game.Sprites;
public abstract class FuselageSprites : SpriteCollection
{
    public enum EngineAnimations
    {
        Default,
        Idle,
        Thrust,
        Reverse
    }
    public FuselageSprites(Fuselage parent) : base(parent, parent.Geometry)
    {
        IsAutomaticAnimation = true;
        Bulk = new Texture(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Bulks,
            ImageFilePaths = BulkImageFilePaths,
        };
        Engine = new Texture(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Engines,
            ImageFilePaths = EngineImageFilePaths,
        };
        Velocity = parent.Velocity;
    }
    public void SetSizeScale(double sizeScale)
    {
        Bulk.SizeScale = sizeScale;
        Engine.SizeScale = sizeScale;
    }
    protected override void Update()
    {
        if (IsAutomaticAnimation)
        {
            EngineAnimation = GetEngineAnimation();
        }
        base.Update();
    }
    protected EngineAnimations GetEngineAnimation()
    {
        if (Velocity is not null && Velocity.Throttle is not null)
        {
            if (Velocity.Throttle > 0)
            {
                return EngineAnimations.Thrust;
            }
            else if (Velocity.Throttle < 0)
            {
                return EngineAnimations.Reverse;
            }
        }
        return EngineAnimations.Idle;
    }
    protected abstract void AnimationChange();
    protected Size Size => Geometry.Size;
    public Texture Bulk { get; }
    public Texture Engine { get; }
    protected Velocity Velocity { get; }
    public bool IsAutomaticAnimation { get; set; }
    private EngineAnimations _engineAnimation;
    public virtual EngineAnimations EngineAnimation
    {
        get => _engineAnimation;
        set
        {
            if (_engineAnimation != value)
            {
                _engineAnimation = value;
                AnimationChange();
            }
        }
    }
    private HardpointCollection _hardpoints;
    public Hardpoint[] GetHardpoints(int index)
    {
        if (_hardpoints is null)
        {
            _hardpoints = NewHardpoints();
            foreach (KeyValuePair<int, Hardpoint[]> hardpointSet in _hardpoints)
            {
                foreach (Hardpoint hardpoint in hardpointSet.Value)
                {
                    hardpoint.Geometry = Parent.Geometry;
                }
            }
        }
        return _hardpoints[index];
    }
    protected abstract HardpointCollection NewHardpoints();
    protected abstract ObservableCollection<string> BulkImageFilePaths { get; }
    protected abstract ObservableCollection<string> EngineImageFilePaths { get; }
    public new Fuselage Parent => (Fuselage)base.Parent;
}
