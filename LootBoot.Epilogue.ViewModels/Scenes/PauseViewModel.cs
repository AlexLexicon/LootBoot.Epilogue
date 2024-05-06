namespace LootBoot.Epilogue.ViewModels;
public class PauseViewModel : ViewModel
{
    public PauseViewModel()
    {
        ItemsViewModels = new ObservableCollection<CollectableViewModel>();
        foreach (Game.Actors.Collectable.RecordDefinition record in Collectables.Records.All)
        {
            ItemsViewModels.Add(new CollectableViewModel(record));
        }
    }
    private void Continue()
    {
        LootBootEpilogue.UnPause();
    }
    private void Options()
    {

    }
    private void QuitToMenu()
    {

    }
    private void QuitToDesktop()
    {
        LootBootEpilogue.Stop();
    }
    public ICommand ContinueCommand => new RelayCommand(Continue);
    public ICommand OptionsCommand => new RelayCommand(Options);
    public ICommand QuitToMenuCommand => new RelayCommand(QuitToMenu);
    public ICommand QuitToDesktopCommand => new RelayCommand(QuitToDesktop);
    private ObservableCollection<CollectableViewModel> _itemsViewModels;
    public ObservableCollection<CollectableViewModel> ItemsViewModels
    {
        get => _itemsViewModels;
        set => Setter(ref _itemsViewModels, value);
    }
}
