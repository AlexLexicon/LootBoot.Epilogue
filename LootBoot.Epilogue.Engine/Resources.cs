namespace LootBoot.Epilogue.Engine;
public static class Resources
{
    public static string GetPath()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
    public static class Images
    {
        private static Dictionary<string, BitmapImage> _pathFileNameImages;
        public static Dictionary<string, BitmapImage> PathFileNameImages => _pathFileNameImages ??= new Dictionary<string, BitmapImage>();
        public static string GetPath()
        {
            string path = Path.Combine(Resources.GetPath(), "Images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static Uri GetUri(string pathFileName) => new(Path.Combine(GetPath(), pathFileName), UriKind.RelativeOrAbsolute);
        public static BitmapImage Get(string pathFileName)
        {
            try
            {
                if (PathFileNameImages.ContainsKey(pathFileName))
                {
                    return PathFileNameImages[pathFileName];
                }
                BitmapImage bitmap = new();
                bitmap.BeginInit();
                bitmap.UriSource = GetUri(pathFileName);
                bitmap.EndInit();
                PathFileNameImages.Add(pathFileName, bitmap);
                return bitmap;
            }
            catch (Exception exception)
            {
                throw new ResourceNotFoundException("Error while fetching image file", exception);
            }
        }
    }
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
