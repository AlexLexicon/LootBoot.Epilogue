namespace LootBoot.Epilogue.ViewModels;
public class CollectableViewModel : ViewModel
{
    private const double SHOWING_SECONDS = 5;
    private DateTime? showingTime;
    public CollectableViewModel(Collectable.RecordDefinition record)
    {
        Record = record;
        Player.Collection.ItemCollected += ItemCollected;
        GameCore.OnUpdate += () =>
        {
            if (IsShowing && Calculations.DateTime.IsSeconds(ref showingTime, SHOWING_SECONDS))
            {
                Hide();
            }
        };
        Title = Record.Name;
        if (!string.IsNullOrWhiteSpace(Record.ImageFilePath))
        {
            try
            {
                ImageSource = Resources.Images.Get(Record.ImageFilePath);
            }
            catch { }
        }
        UpdateCount();
    }
    private void UpdateCount() => Count = $"x{Player.Collection.Get(Record)}";
    private void ItemCollected(Collectable.RecordDefinition record)
    {
        if (Record.Equals(record))
        {
            UpdateCount();
            Show();
        }
    }
    private void Hide()
    {
        IsShowing = false;
    }
    private void Collapse() => IsShowable = false;
    private void Show()
    {
        showingTime = null;
        IsShowable = true;
        IsShowing = true;
    }
    public ICommand ActionCompleteCommand => new RelayCommand(Collapse);
    public Collectable.RecordDefinition Record { get; }
    private bool _isShowable;
    public bool IsShowable
    {
        get => _isShowable;
        set => Setter(ref _isShowable, value);
    }
    private bool _isShowing;
    public bool IsShowing
    {
        get => _isShowing;
        set => Setter(ref _isShowing, value);
    }
    private string _title;
    public string Title
    {
        get => _title;
        set => Setter(ref _title, value);
    }
    private string _count;
    public string Count
    {
        get => _count;
        set => Setter(ref _count, value);
    }
    private BitmapImage _imageSource;
    public BitmapImage ImageSource
    {
        get => _imageSource;
        set => Setter(ref _imageSource, value);
    }
}
