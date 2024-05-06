namespace LootBoot.Epilogue.Engine;
public class ViewportElement : FrameworkElement
{
    public void Create(Sprite sprite)
    {
        if (sprite is not null && !SpriteKeyedSpriteVisual.ContainsKey(sprite))
        {
            SpriteVisual visual = new(sprite);
            SpriteKeyedSpriteVisual.Add(sprite, visual);
            int zIndex = visual.Sprite.ZIndex;
            if (ZIndexKeyedVisualCollections.ContainsKey(zIndex))
            {
                ZIndexKeyedVisualCollections[zIndex].Add(visual);
            }
            else
            {
                System.Windows.Media.VisualCollection visuals = new(this);
                visuals.Add(visual);
                ZIndexKeyedVisualCollections.Add(zIndex, visuals);
            }
        }
    }
    public void Destroy(Sprite sprite)
    {
        if (sprite is not null && SpriteKeyedSpriteVisual.ContainsKey(sprite))
        {
            SpriteVisual visual = SpriteKeyedSpriteVisual[sprite];
            SpriteKeyedSpriteVisual.Remove(sprite);
            int zIndex = visual.Sprite.ZIndex;
            ZIndexKeyedVisualCollections[zIndex].Remove(visual);
        }
    }
    internal void Update()
    {
        foreach (var spriteAndVisual in SpriteKeyedSpriteVisual)
        {
            spriteAndVisual.Value.Update();
        }
    }
    private Dictionary<Sprite, SpriteVisual> SpriteKeyedSpriteVisual { get; } = new Dictionary<Sprite, SpriteVisual>();
    protected SortedDictionary<int, System.Windows.Media.VisualCollection> ZIndexKeyedVisualCollections { get; } = new SortedDictionary<int, System.Windows.Media.VisualCollection>();
    protected override int VisualChildrenCount => SpriteKeyedSpriteVisual.Count;
    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
        if (index < 0 || index >= VisualChildrenCount)
        {
            throw new ArgumentOutOfRangeException();
        }
        int trueIndex;
        int count = 0;
        if (ZIndexKeyedVisualCollections.Count != VisualChildrenCount)
        {
            if (VisualChildrenCount == 1)
            {
                return SpriteKeyedSpriteVisual.First().Value;
            }
        }
        foreach (KeyValuePair<int, System.Windows.Media.VisualCollection> ZIndexAndVisualCollection in ZIndexKeyedVisualCollections)
        {
            trueIndex = index - count;
            count += ZIndexAndVisualCollection.Value.Count;
            if (index < count)
            {
                return ZIndexAndVisualCollection.Value[trueIndex];
            }
        }
        throw new Exception("Failed to get the visual child at the index");
    }
    private class SpriteVisual : System.Windows.Media.DrawingVisual
    {
        private bool isRendered;
        private bool? isPositionChanged;
        private Point drawingPosition;
        private bool? isSizeChanged;
        private Size drawingSize;
        private bool? isAngleChanged;
        private Angle drawingAngle;
        private bool? isContourChanged;
        private Sprite.Contours drawingContour;
        private bool? isBrushChanged;
        private System.Windows.Media.Brush drawingBrush;
        private bool? isPenChanged;
        private System.Windows.Media.Pen drawingPen;
        private bool? isOpacityChanged;
        private double drawingOpacity;
        private bool? isZIndexChanged;
        public int drawingZIndex;
        private bool? isTextChanged;
        public string drawingText;
        private bool? isTypeFaceChanged;
        public string drawingTypeFace;
        private bool? isEmSizeChanged;
        public double drawingEmSize;
        private bool? isPixelsPerDipChanged;
        public double drawingPixelsPerDip;
        private readonly Text _text;
        public SpriteVisual(Sprite sprite)
        {
            VisualBitmapScalingMode = System.Windows.Media.BitmapScalingMode.NearestNeighbor;
            Sprite = sprite;
            if (Sprite is Text text)
            {
                IsText = true;
                _text = text;
            }
            RotateTransform = new System.Windows.Media.RotateTransform();
            DrawingGroup = new System.Windows.Media.DrawingGroup
            {
                Transform = RotateTransform
            };
            GeometryDrawing = new System.Windows.Media.GeometryDrawing();
            DrawingGroup.Children.Add(GeometryDrawing);
            using System.Windows.Media.DrawingContext context = RenderOpen();
            context.DrawDrawing(DrawingGroup);
        }
        public void Update()
        {
            if (Sprite.IsOccluded)
            {
                Opacity = 0;
                isRendered = false;
            }
            else
            {
                DetermineChanges();
                if (IsText)
                {
                    if (isTextChanged.Value || isTypeFaceChanged.Value || isEmSizeChanged.Value || isBrushChanged.Value || isPixelsPerDipChanged.Value || isPositionChanged.Value)
                    {
                        System.Windows.Media.Typeface typeface;
                        try
                        {
                            typeface = new System.Windows.Media.Typeface(drawingTypeFace);
                        }
                        catch
                        {
                            typeface = new System.Windows.Media.Typeface("Consolas");
                        }
                        System.Windows.Media.FormattedText formattedText = new(drawingText ?? string.Empty, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, typeface, drawingEmSize, drawingBrush, drawingPixelsPerDip);
                        Point textLocation = new(drawingPosition.X - formattedText.WidthIncludingTrailingWhitespace / 2, drawingPosition.Y - formattedText.Height / 2);
                        using System.Windows.Media.DrawingContext context = DrawingGroup.Open();
                        context.DrawText(formattedText, textLocation);
                    }
                }
                else
                {
                    if (isPositionChanged.Value || isSizeChanged.Value || isContourChanged.Value)
                    {
                        GenerateGeometry();
                    }
                    if (isBrushChanged.Value)
                    {
                        GeometryDrawing.Brush = drawingBrush;
                    }
                    if (isPenChanged.Value)
                    {
                        GeometryDrawing.Pen = drawingPen;
                    }
                }
                if (isAngleChanged.Value)
                {
                    RotateTransform.Angle = drawingAngle;
                }
                if (!isRendered || isOpacityChanged.Value)
                {
                    Opacity = drawingOpacity;
                }
                isRendered = true;
            }
        }
        private void DetermineChanges()
        {
            IsChange(ref isAngleChanged, Sprite.Geometry.Angle, ref drawingAngle);
            IsChange(ref isBrushChanged, Sprite.Brush, ref drawingBrush);
            IsChange(ref isOpacityChanged, GetSpriteOpacity(), ref drawingOpacity);
            IsChange(ref isZIndexChanged, Sprite.ZIndex, ref drawingZIndex);
            if (IsText)
            {
                DetermineTextChanges();
            }
            else
            {
                DetermineNonTextChanges();
            }
        }
        private void DetermineTextChanges()
        {
            IsChange(ref isPositionChanged, Sprite.RenderBounds.ToCenter(), ref drawingPosition);
            IsChange(ref isTextChanged, _text.Value, ref drawingText);
            IsChange(ref isTypeFaceChanged, _text.TypeFace, ref drawingTypeFace);
            IsChange(ref isEmSizeChanged, _text.EmSize, ref drawingEmSize);
            IsChange(ref isPixelsPerDipChanged, _text.PixelsPerDip, ref drawingPixelsPerDip);
        }
        private void DetermineNonTextChanges()
        {
            IsChange(ref isPositionChanged, Sprite.RenderBounds.Location, ref drawingPosition);
            IsChange(ref isSizeChanged, Sprite.RenderBounds.Size, ref drawingSize);
            IsChange(ref isContourChanged, Sprite.Contour, ref drawingContour);
            IsChange(ref isPenChanged, Sprite.Pen, ref drawingPen);
        }
        private void GenerateGeometry()
        {
            Rect bounds = new(drawingPosition, drawingSize);
            Point center = bounds.ToCenter();
            RotateTransform.CenterX = center.X;
            RotateTransform.CenterY = center.Y;
            switch (Sprite.Contour)
            {
                case Sprite.Contours.Ellipse:
                    SetDrawingToEllipse(bounds, center);
                    break;
                case Sprite.Contours.Triangle:
                    SetDrawingToTriangle(bounds);
                    break;
                case Sprite.Contours.Rectangle:
                default:
                    SetDrawingToRectangle(bounds);
                    break;
            }
        }
        private void SetDrawingToRectangle(Rect bounds)
        {
            if (GeometryDrawing.Geometry is not System.Windows.Media.RectangleGeometry)
            {
                GeometryDrawing.Geometry = new System.Windows.Media.RectangleGeometry();
            }
            System.Windows.Media.RectangleGeometry rectangleGeometry = (System.Windows.Media.RectangleGeometry)GeometryDrawing.Geometry;
            rectangleGeometry.Rect = bounds;
        }
        private void SetDrawingToEllipse(Rect bounds, Point center)
        {
            if (GeometryDrawing.Geometry is not System.Windows.Media.EllipseGeometry)
            {
                GeometryDrawing.Geometry = new System.Windows.Media.EllipseGeometry();
            }
            System.Windows.Media.EllipseGeometry ellipseGeometry = (System.Windows.Media.EllipseGeometry)GeometryDrawing.Geometry;
            ellipseGeometry.Center = center;
            ellipseGeometry.RadiusX = bounds.Width / 2;
            ellipseGeometry.RadiusY = bounds.Height / 2;
        }
        private void SetDrawingToTriangle(Rect bounds)
        {
            if (GeometryDrawing.Geometry is not System.Windows.Media.PathGeometry)
            {
                TriangeGeometry = new TriangeGeometryHost();
                GeometryDrawing.Geometry = TriangeGeometry.PathGeometry;
            }
            TriangeGeometry.Rect = bounds;
        }
        private void IsChange<T>(ref bool? isChanged, T spriteValue, ref T drawingValue)
        {
            if (isChanged is null)
            {
                drawingValue = spriteValue;
                isChanged = true;
            }
            else if (!(spriteValue is null && drawingValue is null || spriteValue is not null && spriteValue.Equals(drawingValue)))
            {
                isChanged = true;
                drawingValue = spriteValue;
            }
            else
            {
                isChanged = false;
            }
        }
        public Sprite Sprite { get; }
        private bool IsText { get; }
        private System.Windows.Media.DrawingGroup DrawingGroup { get; set; }
        private System.Windows.Media.GeometryDrawing GeometryDrawing { get; set; }
        private TriangeGeometryHost TriangeGeometry { get; set; }
        private System.Windows.Media.RotateTransform RotateTransform { get; }
        private double GetSpriteOpacity() => Sprite.IsVisible ? Sprite.Opacity : 0;
        private class TriangeGeometryHost
        {
            public TriangeGeometryHost()
            {
                if (PolyLineSegment is null)
                {
                    PolyLineSegment = new();
                }
                if (PathFigure is null)
                {
                    PathFigure = new()
                    {
                        Segments = new()
                        {
                            PolyLineSegment,
                        },
                    };
                }
                if (PathGeometry is null)
                {
                    PathGeometry = new()
                    {
                        Figures = new()
                        {
                            PathFigure,
                        },
                    };
                }
            }
            private void Update()
            {
                PathFigure.StartPoint = A;
                PolyLineSegment.Points = new()
                {
                    A,
                    B,
                    C,
                    A,
                };
            }
            private Rect _rect;
            public Rect Rect
            {
                get => _rect;
                set
                {
                    if (_rect != value)
                    {
                        _rect = value;
                        A = Rect.BottomLeft;
                        B = new(Rect.Left + Rect.Width / 2, Rect.Top);
                        C = Rect.BottomRight;
                        Update();
                    }
                }
            }
            public Point A { get; private set; }
            public Point B { get; private set; }
            public Point C { get; private set; }
            public System.Windows.Media.PathGeometry PathGeometry { get; set; }
            private System.Windows.Media.PathFigure PathFigure { get; set; }
            private System.Windows.Media.PolyLineSegment PolyLineSegment { get; set; }

        }
    }
}
