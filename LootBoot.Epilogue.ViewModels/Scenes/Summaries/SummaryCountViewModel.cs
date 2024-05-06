namespace LootBoot.Epilogue.ViewModels;
public class SummaryCountViewModel : ViewModel
{
    public SummaryCountViewModel(SummaryDefinition summary, uint count)
    {
        Title = summary.Name;
        Description = $"x{count}";
        if (!string.IsNullOrWhiteSpace(summary.ImageFilePath))
        {
            try
            {
                ImageSource = Resources.Images.Get(summary.ImageFilePath);
            }
            catch { }
        }
        ImagePadding = summary.Category == SummaryDefinition.Categories.Items ? 50 : 0;
    }
    private string _title;
    public string Title
    {
        get => _title;
        set => Setter(ref _title, value);
    }
    private string _description;
    public string Description
    {
        get => _description;
        set => Setter(ref _description, value);
    }
    private BitmapImage _imageSource;
    public BitmapImage ImageSource
    {
        get => _imageSource;
        set => Setter(ref _imageSource, value);
    }
    private double _imagePadding;
    public double ImagePadding
    {
        get => _imagePadding;
        set => Setter(ref _imagePadding, value);
    }
}
