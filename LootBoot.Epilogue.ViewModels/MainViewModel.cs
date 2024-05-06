namespace LootBoot.Epilogue.ViewModels;
public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        SceneManager.ShowMainMenu += ShowMainMenu;
        SceneManager.ShowZone += ShowZone;
        SceneManager.ShowBattle += ShowBattle;
        SceneManager.ShowTally += ShowSummary;
        LootBootEpilogue.GameConsoleToggled -= ToggleGameConsole;
        LootBootEpilogue.GameConsoleToggled += ToggleGameConsole;
        if (TestScene.SHOW)
        {
            SceneManager.Show<TestScene>();
        }
        else
        {
            SceneManager.Show<StarFieldScene>();
            SceneManager.Show<MenuScene>();
        }
    }
    public TViewModel Show<TViewModel>(Scene scene)
    {
        return (TViewModel)Activator.CreateInstance(typeof(TViewModel), new object[] { scene });
    }
    public void ShowMainMenu(Scene scene)
    {
        ZoneViewModel = null;
        BattleViewModel = null;
        MainMenuViewModel = Show<MenuViewModel>(scene);
    }
    public void ShowZone(Scene scene)
    {
        MainMenuViewModel = null;
        ZoneViewModel = Show<ZoneViewModel>(scene);
    }
    public void ShowBattle(Scene scene)
    {
        MainMenuViewModel = null;
        BattleViewModel = Show<BattleViewModel>(scene);
    }
    public void ShowSummary(Scene scene)
    {
        BattleViewModel = null;
        ZoneViewModel = null;
        SummaryViewModel = Show<SummaryViewModel>(scene);
    }
    public void ToggleGameConsole() => GameConsoleViewModel = GameConsoleViewModel is null ? new GameConsoleViewModel() : null;
    private MenuViewModel _mainMenuViewModel;
    public MenuViewModel MainMenuViewModel
    {
        get => _mainMenuViewModel;
        set => Setter(ref _mainMenuViewModel, value);
    }
    private ZoneViewModel _zoneViewModel;
    public ZoneViewModel ZoneViewModel
    {
        get => _zoneViewModel;
        set => Setter(ref _zoneViewModel, value);
    }
    private BattleViewModel _battleViewModel;
    public BattleViewModel BattleViewModel
    {
        get => _battleViewModel;
        set => Setter(ref _battleViewModel, value);
    }
    private SummaryViewModel _summaryViewModel;
    public SummaryViewModel SummaryViewModel
    {
        get => _summaryViewModel;
        set => Setter(ref _summaryViewModel, value);
    }
    private GameConsoleViewModel _gameConsoleViewModel;
    public GameConsoleViewModel GameConsoleViewModel
    {
        get => _gameConsoleViewModel;
        set => Setter(ref _gameConsoleViewModel, value);
    }
}
