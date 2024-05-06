namespace LootBoot.Epilogue.ViewModels.App;
public static class Settings
{
    public static void Save() => Properties.Settings.Default.Save();
    public static class Window
    {
        public static double Left
        {
            get => Properties.Settings.Default.Window_Left;
            set => Properties.Settings.Default.Window_Left = Math.Clamp(value, 16, int.MaxValue);
        }
        public static double Top
        {
            get => Properties.Settings.Default.Window_Top;
            set => Properties.Settings.Default.Window_Top = Math.Clamp(value, 16, int.MaxValue);
        }
        public static double Width
        {
            get => Properties.Settings.Default.Window_Width;
            set => Properties.Settings.Default.Window_Width = Math.Clamp(value, 800, int.MaxValue);
        }
        public static double Height
        {
            get => Properties.Settings.Default.Window_Height;
            set => Properties.Settings.Default.Window_Height = Math.Clamp(value, 600, int.MaxValue);
        }
        public static bool Maximized
        {
            get => Properties.Settings.Default.Window_Maximized;
            set => Properties.Settings.Default.Window_Maximized = value;
        }
    }
}
