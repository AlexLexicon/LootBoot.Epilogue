namespace LootBoot.Epilogue.ViewModels.Console;
public class GameConsoleViewModel : ViewModel, GameConsole.IGameConsole
{
    public GameConsoleViewModel()
    {
        ObservableMessages = new ObservableCollection<string>();
        GameConsole.Console = this;
        RefreshActors();
    }
    public void Write(string message) => Application.Current?.Dispatcher?.Invoke(() => ObservableMessages.Insert(0, message));
    public void Monitor(object obj) { }
    public void EnterCommandMessage()
    {
        GameConsole.Command(CommandMessage);
        CommandMessage = string.Empty;
    }
    public void RefreshActors()
    {
        ObservableGameConsoleEntityItems = new ObservableCollection<GameConsoleActorItemViewModel>();
        LootBootEpilogue.Commands.CompileActors();
        var entityIdentifiers = LootBootEpilogue.Commands.ActorWithIdentifiers;
        foreach (var entityIdentifier in entityIdentifiers)
        {
            GameConsoleActorItemViewModel gameConsoleEntityItemViewModel = new GameConsoleActorItemViewModel(entityIdentifier.Key);
            ObservableGameConsoleEntityItems.Add(gameConsoleEntityItemViewModel);
        }
    }
    public ICommand EnterCommandMessageCommand => new RelayCommand(EnterCommandMessage);
    public ICommand RefreshCommand => new RelayCommand(RefreshActors);
    public List<string> Messages => ObservableMessages.ToList();
    private ObservableCollection<string> _observableMessages;
    public ObservableCollection<string> ObservableMessages
    {
        get => _observableMessages;
        set => Setter(ref _observableMessages, value);
    }
    private ObservableCollection<GameConsoleActorItemViewModel> _observableGameConsoleEntityItems;
    public ObservableCollection<GameConsoleActorItemViewModel> ObservableGameConsoleEntityItems
    {
        get => _observableGameConsoleEntityItems;
        set => Setter(ref _observableGameConsoleEntityItems, value);
    }
    private string _commandMessage;
    public string CommandMessage
    {
        get => _commandMessage;
        set => Setter(ref _commandMessage, value);
    }
}
