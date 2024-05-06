namespace LootBoot.Epilogue.Game.Entities;
public abstract class Fuselage : Entity
{
    protected Fuselage(Spacecraft parent, GameTeam team, FuselageStats stats, FuselageLimits limits) : base(parent, null, GameSequencer.Permanence.Newest)
    {
        Geometry.Size = new PVSize(limits.PVSizeWidthScale);
        Attributes = new FuselageAttributes(this, stats, limits);
        Defense = new Defense(this, team, Geometry);
        Defense.OnKill += () =>
        {
            Parent.Kill();
        };
        Defense.OnMiss += () =>
        {
            AlertIndicator.Show("MISSED!");
        };
        AlertIndicator = new AlertIndicator(this, team.Color);
        if (team.IsPositionTracked)
        {
            PositionIndicator = new PositionIndicator(this, team.Color, PositionIndicator.Sizes.Medium);
        }
        if (team.IsIntegrityVisible)
        {
            IntegrityIndicator = new IntegrityIndicator(this, team.Color);
        }
    }
    protected override void Create()
    {
        Animator = new SpriteAnimator(this)
        {
            Sprites,
        };
        ResetTranslations();
        base.Create();
    }
    public void ResetTranslations()
    {
        Velocity.Set(Attributes.Velocity);
        Rotation.Set(Attributes.Rotation);
    }
    public new Spacecraft Parent => (Spacecraft)base.Parent;
    public FuselageAttributes Attributes { get; }
    public Defense Defense { get; }
    public FuselageSprites Sprites { get; protected set; }
    public AlertIndicator AlertIndicator { get; }
    public PositionIndicator PositionIndicator { get; }
    public IntegrityIndicator IntegrityIndicator { get; }
    public SpriteAnimator Animator { get; private set; }
}
