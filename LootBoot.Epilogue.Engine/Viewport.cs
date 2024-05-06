namespace LootBoot.Epilogue.Engine;
public static class Viewport
{
    public static bool IsOccluded(Geometry geometry) => geometry is not null && IsOccluded(geometry.Bounds.Screen);
    public static bool IsOccluded(Rect screenBounds) => !Geometry.Bounds.Screen.IntersectsWith(screenBounds);
    public static void Update()
    {
        Geometry.EngineUpdate();
        Watched.Update();
    }
    public static class Watched
    {
        internal static void Update()
        {
            if (Geometry is not null)
            {
                Viewport.Geometry.Position = Geometry.Position;
            }
        }
        public static void Set(Geometry geometry) => Geometry = geometry;
        private static Geometry _geometry;
        public static Geometry Geometry
        {
            get => _geometry;
            set
            {
                if (_geometry != value)
                {
                    _geometry = value;
                }
            }
        }
    }
    private static Geometry _geometry;
    public static Geometry Geometry
    {
        get
        {
            if (_geometry is null)
            {
                _geometry = new Geometry(null);
                _geometry.OnChange += (eventArgs) =>
                {
                    if (eventArgs.IsSizeChanged)
                    {
                        Size size = _geometry.Size;
                        Application.Current?.Dispatcher?.Invoke(() =>
                        {
                            Draw.Element.Width = size.Width;
                            Draw.Element.Height = size.Height;
                        });
                    }
                };
            }
            return _geometry;
        }
    }
    public static class Draw
    {
        internal static System.Windows.Media.ImageBrush GetImageBrush(string filePath)
        {

            if (FilePathKeyedImageBrush.ContainsKey(filePath))
            {
                return FilePathKeyedImageBrush[filePath];
            }
            else
            {
                System.Windows.Media.ImageBrush brush = null;
                Application.Current?.Dispatcher?.Invoke(() =>
                {
                    BitmapImage bitmapImage = Resources.Images.Get(filePath);
                    brush = new System.Windows.Media.ImageBrush(bitmapImage);
                    FilePathKeyedImageBrush.Add(filePath, brush);
                });
                return brush;
            }
        }
        internal static void Create(Sprite sprite) => Application.Current?.Dispatcher?.Invoke(() => Element.Create(sprite));
        internal static void Destroy(Sprite sprite) => Application.Current?.Dispatcher?.Invoke(() => Element.Destroy(sprite));
        internal static void Update() => Application.Current?.Dispatcher?.Invoke(Element.Update);
        public static ViewportElement Element { get; } = new ViewportElement();
        private static Dictionary<string, System.Windows.Media.ImageBrush> FilePathKeyedImageBrush { get; } = new Dictionary<string, System.Windows.Media.ImageBrush>();
        public static class Debug
        {
            public static bool IsEnabled { get; set; }
        }
    }
}
