namespace LootBoot.Epilogue.ViewModels;
public class ZoneViewModel : SceneViewModel<ZoneScene>
{
    private bool _isCompleted;
    public ZoneViewModel(ZoneScene scene) : base(scene)
    {
        scene.Completed += () =>
        {
            _isCompleted = true;
            IsZoneComplete = true;
            IsZoneCompletedVisible = true;
            ZoneIndex = Player.Zones.Current.Index.ToString();
        };
        scene.Transitioned += () =>
        {
            IsZoneComplete = false;
        };
        Player.Actor.Hit += () =>
        {
            if (IsPlayerHitToggle is null)
            {
                IsPlayerHitToggle = true;
            }
            IsPlayerHitToggle = !IsPlayerHitToggle;
        };
        LootBootEpilogue.OnPause += () =>
        {
            PauseViewModel = new PauseViewModel();
            foreach (CollectableViewModel item in ItemsViewModels)
            {
                item.IsShowable = false;
            }
        };
        LootBootEpilogue.OnUnPause += () =>
        {
            PauseViewModel = null;
        };
        IsZoneCompletedVisible = false;
        IsZoneComplete = false;
        _isCompleted = false;
        ItemsViewModels = new ObservableCollection<CollectableViewModel>();
        foreach (Game.Actors.Collectable.RecordDefinition record in Collectables.Records.All)
        {
            ItemsViewModels.Add(new CollectableViewModel(record));
        }
    }
    public void ActionComplete()
    {
        if (_isCompleted)
        {
            SceneManager.Show<SummaryScene>();
        }
    }
    public ICommand ActionCompleteCommand => new RelayCommand(ActionComplete);
    private ObservableCollection<CollectableViewModel> _itemsViewModels;
    public ObservableCollection<CollectableViewModel> ItemsViewModels
    {
        get => _itemsViewModels;
        set => Setter(ref _itemsViewModels, value);
    }
    private PauseViewModel _pauseViewModel;
    public PauseViewModel PauseViewModel
    {
        get => _pauseViewModel;
        set => Setter(ref _pauseViewModel, value);
    }
    private bool? _isPlayerHitToggle;
    public bool? IsPlayerHitToggle
    {
        get => _isPlayerHitToggle;
        set => Setter(ref _isPlayerHitToggle, value);
    }
    private bool _isZoneCompletedVisible;
    public bool IsZoneCompletedVisible
    {
        get => _isZoneCompletedVisible;
        set => Setter(ref _isZoneCompletedVisible, value);
    }
    private bool _isZoneComplete;
    public bool IsZoneComplete
    {
        get => _isZoneComplete;
        set => Setter(ref _isZoneComplete, value);
    }
    private string _zoneIndex;
    public string ZoneIndex
    {
        get => _zoneIndex;
        set => Setter(ref _zoneIndex, value);
    }
}
