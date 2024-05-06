namespace LootBoot.Epilogue.ViewModels;
public class SummaryViewModel : ViewModel
{
    public SummaryViewModel(SummaryScene scene)
    {
        SpacecraftSummariesViewModels = new ObservableCollection<SummaryCountViewModel>();
        ItemsSummariesViewModels = new ObservableCollection<SummaryCountViewModel>();
        ZoneIndex = Player.Zones.Current.Index.ToString();
        scene.OnShow += () =>
        {
            IsCreated = true;
            Application.Current?.Dispatcher?.Invoke(Populate);
        };
        IsCreated = false;
    }
    public void Populate()
    {
        ZoneSummary zoneSummary = Player.Zones.Current.Summaries.Get();
        PopulateSummary(SpacecraftSummariesViewModels, SummaryDefinition.Categories.Spacecrafts, zoneSummary);
        PopulateSummary(ItemsSummariesViewModels, SummaryDefinition.Categories.Items, zoneSummary);
    }
    public void PopulateSummary(ObservableCollection<SummaryCountViewModel> itemViewModels, SummaryDefinition.Categories category, ZoneSummary zoneSummary)
    {
        if (zoneSummary is not null)
        {
            IOrderedEnumerable<KeyValuePair<SummaryDefinition, uint>> summaries = zoneSummary.Where(s => s.Key.Category == category).OrderByDescending(s => s.Value);
            foreach (KeyValuePair<SummaryDefinition, uint> summary in summaries)
            {
                itemViewModels.Add(new(summary.Key, summary.Value));
            }
        }
    }
    public void Next()
    {

    }
    public ICommand NextCommand => new RelayCommand(Next);
    private bool _isCreated;
    public bool IsCreated
    {
        get => _isCreated;
        set => Setter(ref _isCreated, value);
    }
    private string _zoneIndex;
    public string ZoneIndex
    {
        get => _zoneIndex;
        set => Setter(ref _zoneIndex, value);
    }
    private ObservableCollection<SummaryCountViewModel> _spacecraftSummariesViewModels;
    public ObservableCollection<SummaryCountViewModel> SpacecraftSummariesViewModels
    {
        get => _spacecraftSummariesViewModels;
        set => Setter(ref _spacecraftSummariesViewModels, value);
    }
    private ObservableCollection<SummaryCountViewModel> _itemsSummariesViewModels;
    public ObservableCollection<SummaryCountViewModel> ItemsSummariesViewModels
    {
        get => _itemsSummariesViewModels;
        set => Setter(ref _itemsSummariesViewModels, value);
    }
}
