namespace LootBoot.Epilogue.Engine;
public static class Input
{
    public static Point MousePosition
    {
        get
        {
            Point result;
            Application.Current?.Dispatcher?.Invoke(() => result = Viewport.Draw.Element is null ? result : Mouse.GetPosition(Viewport.Draw.Element));
            return result.ToWorld();
        }
    }
    public static bool IsKeyDown(Key key)
    {
        bool result = false;
        Application.Current?.Dispatcher?.Invoke(() => result = Keyboard.IsKeyDown(key));
        return result;
    }
    public static bool IsKeyUp(Key key)
    {
        bool result = false;
        Application.Current?.Dispatcher?.Invoke(() => result = Keyboard.IsKeyUp(key));
        return result;
    }
    public static bool IsKeyReleased(Key key)
    {
        if (IsKeyDown(key))
        {
            if (!PreviousKeysDown.Contains(key))
            {
                PreviousKeysDown.Add(key);
            }
        }
        else if (PreviousKeysDown.Contains(key))
        {
            PreviousKeysDownToRemove.Add(key);
            return true;
        }
        return false;
    }
    internal static void Update()
    {
        if (PreviousKeysDownToRemove.Count > 0)
        {
            foreach (Key key in PreviousKeysDownToRemove)
            {
                if (PreviousKeysDown.Contains(key))
                {
                    PreviousKeysDown.Remove(key);
                }
            }
            PreviousKeysDownToRemove.Clear();
        }
    }
    private static HashSet<Key> _previousKeysDownToRemove;
    private static HashSet<Key> PreviousKeysDownToRemove => _previousKeysDownToRemove ??= new HashSet<Key>();
    private static HashSet<Key> _previousKeysDown;
    private static HashSet<Key> PreviousKeysDown => _previousKeysDown ??= new HashSet<Key>();
}
