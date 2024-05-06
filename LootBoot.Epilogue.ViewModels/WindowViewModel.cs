namespace LootBoot.Epilogue.ViewModels;
public class WindowViewModel : ViewModel
{
    public WindowViewModel()
    {
        LootBootEpilogue.OnStop += Stop;
        LootBootEpilogue.OnUnhandledException += Crash;
        LoadWindow();
        LootBootEpilogue.Start();
        StartupViewModel = new StartupViewModel();
        MainViewModel = new MainViewModel();
        StartupViewModel.Loaded += () => StartupViewModel = null;
    }
    public void LoadWindow()
    {
        Title = App.Constants.Window.Title;
        Left = App.Settings.Window.Left;
        Top = App.Settings.Window.Top;
        Width = App.Settings.Window.Width;
        Height = App.Settings.Window.Height;
        WindowState = App.Settings.Window.Maximized ? WindowState.Maximized : WindowState.Normal;
    }
    public void CloseWindow()
    {
        App.Settings.Window.Left = Left;
        App.Settings.Window.Top = Top;
        App.Settings.Window.Width = Width;
        App.Settings.Window.Height = Height;
        App.Settings.Window.Maximized = WindowState == WindowState.Maximized;
        App.Settings.Save();
    }
    public void Stop() => InvokeClose = true;
    public void Crash(string message)
    {
        CrashViewModel = new CrashViewModel(message);
        CrashViewModel.OnQuit += () => LootBootEpilogue.Stop();
    }
    public ICommand LoadedCommand => new RelayCommand(LoadWindow);
    public ICommand ClosedCommand => new RelayCommand(CloseWindow);
    private CrashViewModel _crashViewModel;
    public CrashViewModel CrashViewModel
    {
        get => _crashViewModel;
        set => Setter(ref _crashViewModel, value);
    }
    private StartupViewModel _startupViewModel;
    public StartupViewModel StartupViewModel
    {
        get => _startupViewModel;
        set => Setter(ref _startupViewModel, value);
    }
    private MainViewModel _mainViewModel;
    public MainViewModel MainViewModel
    {
        get => _mainViewModel;
        set => Setter(ref _mainViewModel, value);
    }
    private bool _invokeClose;
    public bool InvokeClose
    {
        get => _invokeClose;
        set => Setter(ref _invokeClose, value);
    }
    private string _title;
    public string Title
    {
        get => _title;
        set => Setter(ref _title, value);
    }
    private double _left;
    public double Left
    {
        get => _left;
        set => Setter(ref _left, value);
    }
    private double _top;
    public double Top
    {
        get => _top;
        set => Setter(ref _top, value);
    }
    private double _width;
    public double Width
    {
        get => _width;
        set => Setter(ref _width, value);
    }
    private double _height;
    public double Height
    {
        get => _height;
        set => Setter(ref _height, value);
    }
    private WindowState _windowState;
    public WindowState WindowState
    {
        get => _windowState;
        set => Setter(ref _windowState, value);
    }
}
