namespace LootBoot.Epilogue.Engine;
public abstract class Sprite : Script
{
    public enum Contours
    {
        Rectangle,
        Triangle,
        Ellipse,
    }
    public Sprite(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public Sprite(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, permanence)
    {
        Geometry = new Geometry(this, baseGeometry);
        Contour = Contours.Rectangle;
        IsVisible = true;
        SizeScale = 1;
        PerspectiveScale = 1;
        Opacity = 1;
    }
    protected override void Create()
    {
        Viewport.Draw.Create(this);
        base.Create();
    }
    protected override void Destroy()
    {
        Viewport.Draw.Destroy(this);
        base.Destroy();
    }
    internal virtual System.Windows.Media.Brush Brush { get; set; }
    internal virtual System.Windows.Media.Pen Pen { get; set; }
    public Geometry Geometry { get; }
    public virtual bool IsOccluded => Viewport.IsOccluded(RenderBounds);
    public virtual Contours Contour { get; set; }
    public virtual bool IsVisible { get; set; }
    public virtual double Opacity { get; set; }
    private int _zIndex;
    public virtual int ZIndex
    {
        get => _zIndex;
        set
        {
            if (_zIndex != value)
            {
                if (IsCreated)
                {
                    Viewport.Draw.Destroy(this);
                }
                _zIndex = value;
                if (IsCreated)
                {
                    Viewport.Draw.Create(this);
                }
            }
        }
    }
    public virtual double SizeScale { get; set; }
    public virtual double PerspectiveScale { get; set; }
    private RenderState? previousRenderState;
    private Rect? _renderBounds;
    public Rect RenderBounds
    {
        get
        {
            RenderState currentRenderState = new()
            {
                SizeScale = SizeScale,
                PerspectiveScale = PerspectiveScale,
                SpritePosition = Geometry.Position,
                ViewportPosition = Viewport.Geometry.Position,
                SpriteSize = Geometry.Size,
                ViewportSize = Viewport.Geometry.Size,
            };
            if (!currentRenderState.Equals(previousRenderState) || _renderBounds is null)
            {
                Point perspectivePosition = Calculations.Geometry.Coordinates.Screen.PositionFromPerspective(currentRenderState.SpritePosition, currentRenderState.PerspectiveScale);
                double width = Math.Max(currentRenderState.SpriteSize.Width * currentRenderState.SizeScale, 0);
                double height = Math.Max(currentRenderState.SpriteSize.Height * currentRenderState.SizeScale, 0);
                double x = perspectivePosition.X - width / 2;
                double y = perspectivePosition.Y - height / 2;
                _renderBounds = new(new Point(x, y), new Size(width, height));
                previousRenderState = currentRenderState;
            }
            return _renderBounds.Value;
        }
    }
    private struct RenderState
    {
        public double SizeScale { get; init; }
        public double PerspectiveScale { get; init; }
        public Point SpritePosition { get; init; }
        public Point ViewportPosition { get; init; }
        public Size SpriteSize { get; init; }
        public Size ViewportSize { get; init; }
    }
}
