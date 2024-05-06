namespace LootBoot.Epilogue.Game.Sprites;
public class IntegritySprite : Script, IEnumerable<Sprite>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Sprite> GetEnumerator() => Marks.GetEnumerator();
    //private const double MARK_SIZE = 4;
    //private const double GAP_SIZE = 3;
    public IntegritySprite(IntegrityIndicator parent, Color color) : base(parent)
    {
        Color = color;
        BaseGeometry = parent.Geometry;
        BaseGeometry.Angle.IsDerivable = false;
        Marks = new List<Shape>();
        IsVisible = true;
    }
    public virtual void SetMarks(int remaining, int total)
    {
        if (total != Marks.Count)
        {
            GenerateMarks(remaining, total);
        }
        else if (remaining != PreviousRemaining)
        {
            for (int index = 0; index < Marks.Count; index++)
            {
                Marks[index].Color = index < remaining ? Color : Color.Transparent;
            }
            PreviousRemaining = remaining;
        }
    }
    protected void SetMarksVisibility()
    {
        for (int index = 0; index < Marks.Count; index++)
        {
            Marks[index].IsVisible = IsVisible;
        }
    }
    protected virtual void GenerateMarks(int remaining, int total)
    {
        foreach (Shape mark in Marks)
        {
            mark.QueueDestroy();
        }
        Marks.Clear();
        double gapSize = new PVSize(0.002).Size.Width;
        double markSize = new PVSize(0.003).Size.Width;
        Size size = FuselageGeometry.Size;
        double y = -(size.Height / 2 + gapSize);
        double totalWidth = size.Width;
        double markDistance = markSize + gapSize;
        int maximumColumns = Math.Max((int)Math.Ceiling(totalWidth / markDistance), 1);
        int rows = (int)Math.Ceiling(total / (double)maximumColumns);
        int marksLeft = total;
        for (int row = 0; row < rows; row++)
        {
            int columns = Math.Min(marksLeft, maximumColumns);
            double x = -((columns - 1) * markDistance / 2);
            for (int column = 0; column < columns; column++)
            {
                Shape mark = new(this, BaseGeometry)
                {
                    ZIndex = (int)Configurations.Sprites.ZIndex.Indicators,
                    Contour = Sprite.Contours.Rectangle,
                    Stroke = Color,
                    StrokeThickness = gapSize / 2,
                };
                mark.Geometry.Size.IsDerivable = false;
                mark.Geometry.Size = new Size(markSize, markSize);
                mark.Geometry.Position = new Point(x, y);
                Marks.Add(mark);
                x += markDistance;
                marksLeft--;
            }
            y += markSize;
        }
        PreviousRemaining = null;
        SetMarks(remaining, total);
    }
    public new IntegrityIndicator Parent => (IntegrityIndicator)base.Parent;
    protected Geometry FuselageGeometry => Parent.Parent.Geometry;
    protected Color Color { get; }
    private Geometry BaseGeometry { get; }
    protected List<Shape> Marks { get; }
    private int? PreviousRemaining { get; set; }
    private bool _isVisible;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                SetMarksVisibility();
            }
        }
    }
}
