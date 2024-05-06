namespace LootBoot.Epilogue.ViewModels;
public class MenuViewModel : SceneViewModel<MenuScene>
{
    private enum Actions
    {
        None,
        Continue,
        New,
        Load,
        Options,
        Secrets,
        Quit,
    }
    public MenuViewModel(MenuScene scene) : base(scene)
    {
        CanContinue = false;
        IsAllowingActions = true;
    }
    public void ActionComplete() => TakeAction(Action, true);
    public void Continue() => SceneManager.Show<ZoneScene>();
    public void New() => SceneManager.Show<ZoneScene>();
    public void Load() { }
    public void Options() { }
    public void Secrets() { }
    public void Quit() => LootBootEpilogue.Stop();
    private bool TakeAction(Actions action, bool complete = false)
    {
        if (IsAllowingActions || complete)
        {
            Action = action;
            IsAllowingActions = false;
            switch (Action)
            {
                case Actions.Continue:
                    if (complete)
                    {
                        Continue();
                    }
                    else
                    {
                        Scene.StartExit();
                    }
                    break;
                case Actions.New:
                    if (complete)
                    {
                        GameConsole.Log("MainMenuViewModel", "New Is Complete");
                        New();
                    }
                    else
                    {
                        GameConsole.Log("MainMenuViewModel", "New Is Not Complete");
                        Scene.StartExit();
                    }
                    break;
                case Actions.Load:
                    if (complete)
                    {
                        Load();
                    }
                    break;
                case Actions.Options:
                    if (complete)
                    {
                        Options();
                    }
                    break;
                case Actions.Secrets:
                    if (complete)
                    {
                        Secrets();
                    }
                    break;
                case Actions.Quit:
                    Quit();
                    break;
                default:
                    IsAllowingActions = true;
                    break;
            }
            return true;
        }
        return false;
    }
    private void UpdateIsContinueActionable() => IsContinueActionable = CanContinue && IsAllowingActions;
    public ICommand ActionCompleteCommand => new RelayCommand(ActionComplete);
    public ICommand ContinueCommand => new RelayCommand(() => TakeAction(Actions.Continue));
    public ICommand NewCommand => new RelayCommand(() => TakeAction(Actions.New));
    public ICommand LoadCommand => new RelayCommand(() => TakeAction(Actions.Load));
    public ICommand OptionsCommand => new RelayCommand(() => TakeAction(Actions.Options));
    public ICommand SecretsCommand => new RelayCommand(() => TakeAction(Actions.Secrets));
    public ICommand QuitCommand => new RelayCommand(() => TakeAction(Actions.Quit));
    private Actions Action { get; set; }
    private bool _isAllowingActions;
    public bool IsAllowingActions
    {
        get => _isAllowingActions;
        set
        {
            Setter(ref _isAllowingActions, value);
            UpdateIsContinueActionable();
        }
    }
    private bool _canContinue;
    public bool CanContinue
    {
        get => _canContinue;
        set
        {
            Setter(ref _canContinue, value);
            UpdateIsContinueActionable();
        }
    }
    private bool _isContinueActionable;
    public bool IsContinueActionable
    {
        get => _isContinueActionable;
        private set => Setter(ref _isContinueActionable, value);
    }
}
