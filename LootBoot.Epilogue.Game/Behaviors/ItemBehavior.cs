namespace LootBoot.Epilogue.Game.Behaviors;
public class ItemBehavior : Behavior
{
    protected override void Create()
    {
        Geometry = GetScript<Geometry>();
        Velocity = GetScript<Velocity>();
        Size? size = Player.Geometry.Size;
        double length;
        if (size is not null)
        {
            length = size.Value.Width;
        }
        else
        {
            PVSize pvSize = new PVSize(0.03125);
            length = pvSize.Size.Width;
        }
        DistanceToCollect = length / 3;
        DistanceToMove = length * 2;
        base.Create();
    }
    protected override void Update()
    {
        double? distanceToPlayer = Player.Geometry.DistanceTo(Geometry);
        if (distanceToPlayer <= DistanceToMove)
        {
            if (distanceToPlayer <= DistanceToCollect)
            {
                Complete();
            }
            else
            {
                Velocity.MoveTo = Player.Geometry.Position;
                Velocity.Throttle = 1;
            }
        }
        else
        {
            Velocity.Throttle = 0;
        }
        base.Update();
    }
    protected double DistanceToMove { get; set; }
    protected double DistanceToCollect { get; set; }
    protected Geometry Geometry { get; set; }
    protected Velocity Velocity { get; set; }
}
