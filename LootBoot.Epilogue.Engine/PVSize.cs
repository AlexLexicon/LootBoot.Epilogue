namespace LootBoot.Epilogue.Engine;
/// <summary>
/// ProportionalViewportSize
/// </summary>
public struct PVSize
{
    public enum ProportionAxis
    {
        Width,
        Height
    }
    public PVSize(double width) : this(width, 1) { }
    public PVSize(double width, double height) : this(width, height, ProportionAxis.Width) { }
    public PVSize(double width, double height, ProportionAxis axis)
    {
        _size = null;
        Width = width;
        Height = height;
        Axis = axis;
    }
    public double Width { get; init; }
    public double Height { get; init; }
    public ProportionAxis Axis { get; init; }
    private Size? _size;
    public Size Size
    {
        get
        {
            if (_size is null)
            {
                Size viewportSize = Viewport.Geometry.Size.Value;
                double trueWidth;
                double trueHeight;
                if (Axis == ProportionAxis.Width)
                {
                    trueWidth = viewportSize.Width * Width;
                    trueHeight = trueWidth * Height;
                }
                else
                {
                    trueHeight = viewportSize.Height * Height;
                    trueWidth = trueHeight * Width;
                }
                _size = new(Math.Max(trueWidth, 0), Math.Max(trueHeight, 0));
            }
            return _size.Value;
        }
    }
    public static implicit operator Size(PVSize pvSize) => pvSize.Size;
    public static implicit operator GeometricSize(PVSize pvSize) => pvSize.Size;
}
