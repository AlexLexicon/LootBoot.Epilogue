namespace LootBoot.Epilogue.Engine.FluentBuilders;
public class RngBoundsFluentBuilder
{
    public RngBoundsFluentBuilder(Rect rect) => Rect = rect;
    public Point Outside(Size size) => Rng.Geomerty.Outside(Rect, size);
    public Point Outside(Size size, Offset area) => Rng.Geomerty.Outside(Rect, size, area);
    public Point Outside(Offset offset, Offset area) => Rng.Geomerty.Outside(Rect, offset, area);
    public Point Inside() => Rng.Geomerty.Inside(Rect);
    private Rect Rect { get; }
}
