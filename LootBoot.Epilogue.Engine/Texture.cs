namespace LootBoot.Epilogue.Engine;
public class Texture : Sprite, ICloneable
{
    private DateTime lastAnimated;
    private bool animationValueChanged;
    public Texture(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public Texture(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, baseGeometry, permanence) { }
    protected override void Update()
    {
        if (IsAnimated && ImageFilePaths is not null && ImageFilePaths.Count > 0 && (animationValueChanged || (DateTime.Now - lastAnimated).TotalSeconds >= AnimationSeconds))
        {
            int maximumImageIndex = AnimationIndexMaximum is not null ? AnimationIndexMaximum.Value : ImageFilePaths.Count - 1;
            int minimumImageIndex = AnimationIndexMinimum is not null ? AnimationIndexMinimum.Value : 0;
            int imageIndex = AnimationIndex;
            if (AnimationIndexDirection == SpriteAnimation.Directions.Ascending)
            {
                imageIndex++;
                if (imageIndex > maximumImageIndex)
                {
                    if (AnimationRepeatBehavior == SpriteAnimation.RepeatBehaviors.Forever)
                    {
                        imageIndex = minimumImageIndex;
                    }
                    else if (AnimationRepeatBehavior == SpriteAnimation.RepeatBehaviors.Reverse)
                    {
                        AnimationIndexDirection = SpriteAnimation.Directions.Descending;
                    }
                }
            }
            else if (AnimationIndexDirection == SpriteAnimation.Directions.Descending)
            {
                imageIndex--;
                if (imageIndex < minimumImageIndex)
                {
                    if (AnimationRepeatBehavior == SpriteAnimation.RepeatBehaviors.Forever)
                    {
                        imageIndex = maximumImageIndex;
                    }
                    else if (AnimationRepeatBehavior == SpriteAnimation.RepeatBehaviors.Reverse)
                    {
                        AnimationIndexDirection = SpriteAnimation.Directions.Ascending;
                    }
                }
            }
            imageIndex = Math.Clamp(imageIndex, minimumImageIndex, maximumImageIndex);
            AnimationIndex = imageIndex;
            animationValueChanged = false;
            lastAnimated = DateTime.Now;
        }
        base.Update();
    }
    protected virtual void GenerateImage()
    {
        if (ImageFilePaths is not null && ImageFilePaths.Count > 0 && AnimationIndex < ImageFilePaths.Count)
        {
            Brush = Viewport.Draw.GetImageBrush(ImageFilePaths[AnimationIndex]);
        }
    }
    public object Clone() => new Texture(Parent, Geometry.BaseGeometry)
    {
        AnimationIndex = AnimationIndex,
        AnimationIndexDirection = AnimationIndexDirection,
        AnimationIndexMaximum = AnimationIndexMaximum,
        AnimationIndexMinimum = AnimationIndexMinimum,
        AnimationRepeatBehavior = AnimationRepeatBehavior,
        AnimationSeconds = AnimationSeconds,
        Contour = Contour,
        ImageFilePaths = new ObservableCollection<string>(ImageFilePaths),
        IsUpdatable = IsUpdatable,
        IsVisible = IsVisible,
        Opacity = Opacity,
        ZIndex = ZIndex,
        PerspectiveScale = PerspectiveScale,
        SizeScale = SizeScale,
    };
    public bool IsAnimated => AnimationSeconds > 0;
    public double AnimationSeconds { get; set; }
    public SpriteAnimation.Directions AnimationIndexDirection { get; set; }
    public SpriteAnimation.RepeatBehaviors AnimationRepeatBehavior { get; set; }
    private int _animationIndex;
    public int AnimationIndex
    {
        get => _animationIndex;
        set
        {
            if (_animationIndex != value)
            {
                _animationIndex = value;
                GenerateImage();
            }
        }
    }
    private int? _animationIndexMaximum;
    public int? AnimationIndexMaximum
    {
        get => _animationIndexMaximum;
        set
        {
            if (_animationIndexMaximum != value)
            {
                _animationIndexMaximum = value;
                animationValueChanged = true;
            }
        }
    }
    private int? _animationIndexMinimum;
    public int? AnimationIndexMinimum
    {
        get => _animationIndexMinimum;
        set
        {
            if (_animationIndexMinimum != value)
            {
                _animationIndexMinimum = value;
                animationValueChanged = true;
            }
        }
    }
    private ObservableCollection<string> _imageFilePaths;
    public ObservableCollection<string> ImageFilePaths
    {
        get => _imageFilePaths;
        set
        {
            if (_imageFilePaths != value)
            {
                _imageFilePaths = value;
                _imageFilePaths.CollectionChanged += (s, e) => GenerateImage();
                GenerateImage();
            }
        }
    }
    internal new System.Windows.Media.ImageBrush Brush
    {
        get => (System.Windows.Media.ImageBrush)base.Brush;
        set => base.Brush = value;
    }
    protected Image Image { get; set; }
}
