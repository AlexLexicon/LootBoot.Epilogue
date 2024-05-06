namespace LootBoot.Epilogue.ViewModels.Console;
public class GameConsoleActorItemViewModel : ViewModel
{
    public GameConsoleActorItemViewModel(Commands.ActorIdentifier entityIdentifier)
    {
        Id = entityIdentifier.Id;
        Name = entityIdentifier.Name;
    }
    private string _id;
    public string Id
    {
        get => _id;
        set => Setter(ref _id, value);
    }
    private string _name;
    public string Name
    {
        get => _name;
        set => Setter(ref _name, value);
    }
}

