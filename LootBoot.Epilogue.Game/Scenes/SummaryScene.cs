namespace LootBoot.Epilogue.Game.Scenes;
public class SummaryScene : Scene
{
    public event Action OnShow;
    private const int SCEONDS_TO_SHOW = 1;
    private DateTime? _secondsToShow;
    public SummaryScene() : base((int)Configurations.Scenes.Layers.Overlay)
    {
        IsShowing = false;
    }
    protected override void Create()
    {
        base.Create();
        LootBootEpilogue.IsPausable = false;
    }
    protected override void Update()
    {
        if (!IsShowing && Calculations.DateTime.IsSeconds(ref _secondsToShow, SCEONDS_TO_SHOW))
        {
            Show();
        }
        base.Update();
    }
    protected void Show()
    {
        IsShowing = true;
        OnShow?.Invoke();
    }
    private bool IsShowing { get; set; }
}
