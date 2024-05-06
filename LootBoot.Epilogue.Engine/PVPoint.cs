namespace LootBoot.Epilogue.Engine;
/// <summary>
/// ProportionalViewportPoint
/// </summary>
public struct PVPoint
{
    public PVPoint(double x, double y)
    {
        _point = null;
        X = x;
        Y = y;
    }
    public double X { get; init; }
    public double Y { get; init; }
    private Point? _point;
    public Point Point
    {
        get
        {
            if (_point is null)
            {
                Size viewportSize = Viewport.Geometry.Size.Value;
                double trueX;
                double trueY;
                trueX = viewportSize.Width / 2 * X;
                trueY = viewportSize.Height / 2 * Y;
                _point = new(trueX, trueY);
            }
            return _point.Value;
        }
    }
    public static implicit operator Point(PVPoint pvPoint) => pvPoint.Point;
    public static implicit operator GeometricPosition(PVPoint pvPoint) => pvPoint.Point;
}
