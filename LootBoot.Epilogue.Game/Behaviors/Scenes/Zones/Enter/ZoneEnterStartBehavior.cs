namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones.Enter;
public class ZoneEnterStartBehavior : ZoneEnterBehavior
{
    private const double VIEWPORT_VELOCITY = 500;
    protected override void Create()
    {
        base.Create();
        Point viewportPosition = Viewport.Geometry.Position;
        Size viewportSize = Viewport.Geometry.Size;
        Size spacecraftSize = Geometry.Size;
        double viewportOffset = Math.Max(viewportSize.Width / 2, viewportSize.Height / 2);
        double spacecraftOffset = Math.Max(spacecraftSize.Width, spacecraftSize.Height);
        double offset = viewportOffset + spacecraftOffset;
        Geometry.Position = new Point(viewportPosition.X - offset, viewportPosition.Y + offset);
        Viewport.Watched.Set(null);
        MinimumDistance = SPACECRAFT_VELOCITY;
        Source.Fuselage.Sprites.SetSizeScale(2);
        Source.Weapon.Sprites.SetSizeScale(2);
    }
    protected override void Update()
    {
        Speed speed = VIEWPORT_VELOCITY;
        Viewport.Geometry.MoveAtAngle(speed);
        double distance = Viewport.Geometry.DistanceToPosition(Geometry);
        if (distance <= MinimumDistance.Distance)
        {
            Complete();
        }
        base.Update();
    }
    protected Speed MinimumDistance { get; private set; }
}
