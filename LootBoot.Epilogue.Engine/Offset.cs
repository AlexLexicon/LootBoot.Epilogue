namespace LootBoot.Epilogue.Engine;
public struct Offset
{
    public Offset(double value) => Left = Right = Top = Bottom = value;
    public Offset(double horizontal, double vertical)
    {
        Left = Right = horizontal;
        Top = Bottom = vertical;
    }
    public Offset(double left, double top, double right, double bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
    public double Left { get; init; }
    public double Top { get; init; }
    public double Right { get; init; }
    public double Bottom { get; init; }
    public static implicit operator Offset(double value) => new(value);
    public static implicit operator Offset((double horizontal, double vertical) value) => new(value.horizontal, value.vertical);
    public static implicit operator Offset((double left, double top, double right, double bottom) value) => new(value.left, value.top, value.right, value.bottom);
}
