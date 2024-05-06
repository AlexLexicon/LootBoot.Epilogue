namespace LootBoot.Epilogue.Game;
public static class LootBootEpilogue
{
    public static event Action GameConsoleToggled;
    public static event Action OnStop;
    public static event Action OnPause;
    public static event Action OnUnPause;
    public static event Action<string> OnUnhandledException;
    public static void Start()
    {
        Commands = new Commands();
        Settings = new Settings();
        GameCore.OnUpdateException += UpdateException;
        GameCore.OnTick += Tick;
        GameCore.OnUpdate += Update;
        GameCore.Start();
        Viewport.Geometry.Size = new Size(1920, 1080);
    }
    public static void Tick()
    {
        if (Input.IsKeyReleased(Key.P) && Input.IsKeyDown(Key.LeftShift))
        {
            GameCore.IsPaused = !GameCore.IsPaused;
        }
        if (IsPausable && Input.IsKeyReleased(Settings.Keybinding.Pause))
        {
            if (GameCore.IsPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }
    public static void Update()
    {
        if (Input.IsKeyReleased(Settings.Keybinding.DebugConsole))
        {
            ToggleGameConsole();
        }
    }
    public static void UpdateException(Exception exception)
    {
        OnUnhandledException?.Invoke(exception?.Message);
    }
    public static void Stop()
    {
        GameCore.Stop();
        OnStop?.Invoke();
    }
    public static void Pause()
    {
        GameCore.IsPaused = true;
        OnPause?.Invoke();
    }
    public static void UnPause()
    {
        GameCore.IsPaused = false;
        OnUnPause?.Invoke();
    }
    private static bool _isPausable;
    public static bool IsPausable
    {
        get => _isPausable;
        set
        {
            _isPausable = value;
            if (!_isPausable && GameCore.IsPaused)
            {
                UnPause();
            }
        }
    }
    public static void ToggleGameConsole() => GameConsoleToggled?.Invoke();
    public static Settings Settings { get; private set; }
    public static Commands Commands { get; private set; }
}
