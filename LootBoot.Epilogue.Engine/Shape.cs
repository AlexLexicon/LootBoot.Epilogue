namespace LootBoot.Epilogue.Engine;
public class Shape : Sprite
{
    public Shape(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public Shape(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, baseGeometry, permanence) { }
    private void NewPen()
    {
        System.Windows.Media.SolidColorBrush brush = Stroke.ToSolidColorBrush();
        brush.Freeze();
        Pen = new System.Windows.Media.Pen(brush, StrokeThickness);
        Pen.Freeze();
    }
    private Color _color;
    public virtual Color Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                Brush = value.ToSolidColorBrush();
                Brush.Freeze();
            }
        }
    }
    private Color _stroke;
    public virtual Color Stroke
    {
        get => _stroke;
        set
        {
            if (_stroke != value)
            {
                _stroke = value;
                NewPen();
            }
        }
    }
    private double _strokeThickness;
    public virtual double StrokeThickness
    {
        get => _strokeThickness;
        set
        {
            if (_strokeThickness != value)
            {
                _strokeThickness = value;
                NewPen();
            }
        }
    }
}
