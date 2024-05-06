namespace LootBoot.Epilogue.Engine;
public static class Extensions
{
    public static PVSize ToPV(this Size size, PVSize.ProportionAxis axis = PVSize.ProportionAxis.Width)
    {
        Size viewportSize = Viewport.Geometry.Size.Value;
        double pvWidth;
        double pvHeight;
        if (axis == PVSize.ProportionAxis.Width)
        {
            pvWidth = size.Width / viewportSize.Width;
            pvHeight = size.Width / size.Height;
        }
        else
        {
            pvHeight = size.Height / viewportSize.Height;
            pvWidth = size.Height / size.Width;
        }
        return new PVSize(pvWidth, pvHeight, axis);
    }
    public static double GetArea(this Rect rect, bool isAllowZero = true)
    {
        double width = rect.Width > 0 ? rect.Width : isAllowZero ? rect.Width : 1;
        double height = rect.Height > 0 ? rect.Height : isAllowZero ? rect.Height : 1;
        return width * height;
    }
    public static RngBoundsFluentBuilder Rng(this Rect bounds) => new RngBoundsFluentBuilder(bounds);
    public static void RotateToPosition(this Geometry geometry, Geometry target) => RotateToPosition(geometry, target.Position);
    public static void RotateToPosition(this Geometry geometry, Point target) => geometry.Angle = Calculations.Geometry.Angle.ToTarget(geometry.Position, target);
    public static void RotateToPosition(this Geometry geometry, Geometry target, Speed speed) => RotateToPosition(geometry, target, speed, out bool isReached);
    public static void RotateToPosition(this Geometry geometry, Geometry target, Speed speed, out bool isReached) => RotateToPosition(geometry, target.Position, speed, out isReached);
    public static void RotateToPosition(this Geometry geometry, Point target, Speed speed) => RotateToPosition(geometry, target, speed, out bool isReached);
    public static void RotateToPosition(this Geometry geometry, Point target, Speed speed, out bool isReached) => RotateToAngle(geometry, Calculations.Geometry.Angle.ToTarget(geometry.Position, target), speed, out isReached);
    public static void RotateToAngle(this Geometry geometry, Geometry target, Speed speed) => RotateToAngle(geometry, target, speed, out bool isReached);
    public static void RotateToAngle(this Geometry geometry, Geometry target, Speed speed, out bool isReached) => RotateToAngle(geometry, target.Angle, speed, out isReached);
    public static void RotateToAngle(this Geometry geometry, Angle target, Speed speed) => RotateToAngle(geometry, target, speed, out bool isReached);
    public static void RotateToAngle(this Geometry geometry, Angle target, Speed speed, out bool isReached)
    {
        Angle angle = Calculations.Geometry.Angle.ToAngle(geometry.Angle, target, speed);
        isReached = angle == target;
        geometry.Angle.Value = angle;
    }
    public static void MoveToPosition(this Geometry geometry, Geometry target, Speed speed) => MoveToPosition(geometry, target, speed, out bool isReached);
    public static void MoveToPosition(this Geometry geometry, Geometry target, Speed speed, out bool isReached) => MoveToPosition(geometry, target.Position, speed, out isReached);
    public static void MoveToPosition(this Geometry geometry, Point target, Speed speed) => MoveToPosition(geometry, target, speed, out bool isReached);
    public static void MoveToPosition(this Geometry geometry, Point target, Speed speed, out bool isReached)
    {
        geometry.Position = Calculations.Geometry.Position.ToTarget(geometry.Position, target, speed);
        isReached = geometry.Position == target;
    }
    public static void MoveAtAngle(this Geometry geometry, Speed speed) => geometry.Position = Calculations.Geometry.Position.AtAngle(geometry.Position.Value, geometry.Angle, speed);
    public static Point ToScreen(this Geometry geometry) => ToScreen(geometry.Position);
    public static Point ToScreen(this Point worldPosition) => Calculations.Geometry.Coordinates.World.PositionToScreen(worldPosition);
    public static Rect ToScreen(this Rect bounds) => new(ToScreen(bounds.Location), bounds.Size);
    public static Point ToWorld(this Geometry geometry) => ToScreen(geometry.Position.Screen);
    public static Point ToWorld(this Point screenPosition) => Calculations.Geometry.Coordinates.Screen.PositionToWorld(screenPosition);
    public static Rect ToWorld(this Rect bounds) => new(ToWorld(bounds.Location), bounds.Size);
    public static Point PositionToTarget(this Geometry geometry, Point target, Speed speed) => ToTarget(geometry.Position, target, speed);
    public static Point ToTarget(this Point position, Point target, Speed speed) => Calculations.Geometry.Position.ToTarget(position, target, speed);
    public static Point PositionFromAngle(this Geometry geometry, Point origin) => FromAngle(geometry.Position, geometry.Angle, origin);
    public static Point FromAngle(this Point position, Angle angle, Point origin) => Calculations.Geometry.Position.FromAngle(position, angle, origin);
    public static Point PositionAtAngle(this Geometry geometry, Speed speed) => AtAngle(geometry.Position, geometry.Angle, speed);
    public static Point AtAngle(this Point position, Angle angle, Speed speed) => Calculations.Geometry.Position.AtAngle(position, angle, speed);
    public static Point AtAngle(this Point position, Angle angle, double distance) => Calculations.Geometry.Position.AtAngle(position, angle, distance);
    public static Angle AngleToTarget(this Geometry geometry, Point target) => AngleToTarget(geometry.Position, target);
    public static Angle AngleToTarget(this Point position, Point target) => Calculations.Geometry.Angle.ToTarget(position, target);
    public static double DistanceToPosition(this Geometry geometry, Geometry target) => DistanceToPosition(geometry, target.Position);
    public static double DistanceToPosition(this Geometry geometry, Point target) => DistanceToPosition(geometry.Position, target);
    public static double DistanceToPosition(this Point position, Geometry geometry) => DistanceToPosition(position, geometry.Position);
    public static double DistanceToPosition(this Point position, Point target) => Calculations.Geometry.Distance.ToTarget(position, target);
    public static Point ToCenter(this Geometry geometry) => ToCenter(geometry.Bounds);
    public static Point ToCenter(this Rect rect) => Calculations.Geometry.Bounds.ToCenter(rect);
    public static Rect Expanded(this Geometry geometry, Offset offset) => Expanded(geometry.Bounds, offset);
    public static Rect Expanded(this Rect rect, Offset offset) => Calculations.Geometry.Bounds.ToExpanded(rect, offset);
    public static Rect AtAngle(this Geometry geometry) => Calculations.Geometry.Bounds.AtAngle(geometry.Bounds, geometry.Angle);
    public static Rect AtAngle(this Rect rect, Angle angle) => Calculations.Geometry.Bounds.AtAngle(rect, angle);
    public static Size Scale(this Size size, double scale) => new(size.Width * scale, size.Height * scale);
}
