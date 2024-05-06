namespace LootBoot.Epilogue.Engine;
public struct Color
{
    public static Color Red => new Color(System.Windows.Media.Colors.Red);
    public static Color White => new Color(System.Windows.Media.Colors.White);
    public static Color Cyan => new Color(System.Windows.Media.Colors.Cyan);
    public static Color DarkGray => new Color(System.Windows.Media.Colors.DarkGray);
    public static Color Yellow => new Color(System.Windows.Media.Colors.Yellow);
    public static Color Transparent => new Color(System.Windows.Media.Colors.Transparent);
    public static Color Pink => new Color(System.Windows.Media.Colors.Pink);
    private readonly System.Windows.Media.Color _mediaColor;
    public Color(int r, int g, int b) : this((byte)r, (byte)g, (byte)b) { }
    public Color(byte r, byte g, byte b) : this(System.Windows.Media.Color.FromRgb(r, g, b)) { }
    public Color(System.Windows.Media.Color mediaColor)
    {
        _mediaColor = mediaColor;
    }
    public byte A => _mediaColor.A;
    public byte R => _mediaColor.R;
    public byte G => _mediaColor.G;
    public byte B => _mediaColor.B;
    public static bool operator ==(Color a, Color b) => a._mediaColor.Equals(b._mediaColor);
    public static bool operator !=(Color a, Color b) => !(a._mediaColor == b._mediaColor);
    public System.Windows.Media.Color ToMediaColor() => _mediaColor;
    public System.Windows.Media.SolidColorBrush ToSolidColorBrush() => new System.Windows.Media.SolidColorBrush(_mediaColor);
    public override bool Equals(object obj)
    {
        if (obj is Color color)
        {
            return _mediaColor.Equals(color._mediaColor);
        }
        return false;
    }
    public override int GetHashCode() => base.GetHashCode();
}
