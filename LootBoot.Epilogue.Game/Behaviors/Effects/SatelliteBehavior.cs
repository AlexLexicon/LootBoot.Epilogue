namespace LootBoot.Epilogue.Game.Behaviors.Effects;
public abstract class SatelliteBehavior : SatelliteBehavior<Satellite> { }
public abstract class SatelliteBehavior<TSatellite> : Behavior<TSatellite> where TSatellite : Satellite
{
    private const double DESPAWN_SECONDS = 1;
    private DateTime? despawnTime;
    private Rect previousViewportBounds;
    private Rect? cachedOuterViewportBounds;
    protected override void Create()
    {
        base.Create();
        Geometry = GetScript<Geometry>();
        Sprite = GetScript<Sprite>();
        Generate();
    }
    protected override void Update()
    {
        if (Sprite.IsOccluded)
        {
            if (Calculations.DateTime.IsSeconds(ref despawnTime, DESPAWN_SECONDS))
            {
                Complete();
            }
        }
        else
        {
            despawnTime = null;
        }
        base.Update();
    }
    protected virtual void Generate()
    {
        Geometry.Size = GenerateSize();
        Sprite.PerspectiveScale = GeneratePerspectiveScale();
        Geometry.Position = GeneratePosition();

        Source.IsGenerated = true;
        despawnTime = null;
    }
    protected abstract Size GenerateSize();
    protected abstract double GeneratePerspectiveScale();
    protected virtual Point GeneratePosition()
    {
        Rect screenBounds = Viewport.Geometry.Bounds.Screen;
        Point spawnScreenPosition = !Source.IsGenerated ? GetSpawnInsidePosition(screenBounds) : GetSpawnOutsidePosition(screenBounds);
        Point worldPosition = Calculations.Geometry.Coordinates.World.PositionFromPerspective(spawnScreenPosition, Sprite.PerspectiveScale);
        return worldPosition;
    }
    protected abstract double OutsideOffsetArea();
    private Point GetSpawnInsidePosition(Rect screenBounds) => screenBounds.Rng().Inside();
    private Point GetSpawnOutsidePosition(Rect screenBounds)
    {
        Size size = Geometry.Bounds.Screen.AtAngle(Geometry.Angle).Size;
        return screenBounds.Rng().Outside(size, OutsideOffsetArea());
    }
    protected abstract Size GetMaximumSize();
    protected virtual Rect OuterViewportBounds
    {
        get
        {
            Rect screenBounds = Viewport.Geometry.Bounds.Screen;
            if (screenBounds != previousViewportBounds || cachedOuterViewportBounds is null)
            {
                Size maximumSize = GetMaximumSize();
                cachedOuterViewportBounds = screenBounds.Expanded(new Offset(Math.Max(maximumSize.Width, maximumSize.Height)));
                previousViewportBounds = screenBounds;
            }
            return cachedOuterViewportBounds.Value;
        }
    }
    protected Geometry Geometry { get; private set; }
    protected Sprite Sprite { get; private set; }
}
