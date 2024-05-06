namespace LootBoot.Epilogue.Engine;
public class Text : Sprite
{
    public Text(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public Text(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, baseGeometry, permanence)
    {
        TypeFace = "";
        EmSize = 12;
        PixelsPerDip = 1;
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
                Brush = new System.Windows.Media.SolidColorBrush(value.ToMediaColor());
                Brush.Freeze();
            }
        }
    }
    public virtual string Value { get; set; }
    public virtual string TypeFace { get; set; }
    public virtual double EmSize { get; set; }
    public virtual double PixelsPerDip { get; set; }
}
