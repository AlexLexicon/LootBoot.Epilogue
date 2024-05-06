namespace LootBoot.Epilogue.ViewModels;
public class StartupViewModel : ViewModel
{
    private enum Actions
    {
        Loading,
        Complete,
    }
    public event Action Loaded;
    public StartupViewModel()
    {
        IsActive = true;
        Action = Actions.Loading;
        Load();
    }
    private void Load()
    {
        if (Timer is null)
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            Timer.Tick += (s, e) => Load();
            Timer.Start();
        }
        else
        {
            Timer.Stop();
            Timer = null;
            if (Action == Actions.Loading)
            {
                IsActive = false;
            }
            else if (Action == Actions.Complete)
            {
                Loaded?.Invoke();
            }
        }
    }
    private DispatcherTimer Timer { get; set; }
    public void LoadingComplete()
    {
        Action = Actions.Complete;
        Load();
    }
    public ICommand LoadingCompleteCommand => new RelayCommand(LoadingComplete);
    private Actions Action { get; set; }
    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set => Setter(ref _isActive, value);
    }
}
