namespace LootBoot.Epilogue.Game.Entities;
public abstract class Weapon : Entity
{
    public Weapon(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats, WeaponLimits limits) : base(parent, baseGeometry, GameSequencer.Permanence.Newest)
    {
        Geometry.OnChange += GeometryChange;
        Geometry.Size.IsDerivable = false;
        Geometry.Size = new PVSize(limits.PVSizeWidthScale);
        Attributes = new WeaponAttributes(this, stats, limits);
        Hardpoints = hardpoints;
        Team = team;
        Animator = new SpriteAnimator(this);
        Animator.OnAnimationStep += UpdateSprites;
    }
    protected virtual void GeometryChange(Geometry.ChangedEventArgs eventArgs)
    {
        if (eventArgs.IsSizeChanged)
        {
            UpdateSprites();
        }
    }
    protected virtual void UpdateSprites() => _sprites.UpdateSprites(Hardpoints);
    protected virtual void SpritesReGenerated()
    {
        Animator.Stop();
        Animator.Clear();
        Animator.Add(Sprites);
    }
    public new Spacecraft Parent => (Spacecraft)base.Parent;
    public WeaponAttributes Attributes { get; }
    public Hardpoint[] Hardpoints { get; }
    public GameTeam Team { get; }
    private WeaponSprites _sprites;
    public WeaponSprites Sprites
    {
        get => _sprites;
        protected set
        {
            if (_sprites != value)
            {
                if (_sprites is not null)
                {
                    _sprites.ReGenerated -= SpritesReGenerated;
                }
                _sprites = value;
                if (_sprites is not null)
                {
                    _sprites.ReGenerated += SpritesReGenerated;
                }
                UpdateSprites();
            }
        }
    }
    public bool IsActivated { get; set; }
    public SpriteAnimator Animator { get; private set; }
}
