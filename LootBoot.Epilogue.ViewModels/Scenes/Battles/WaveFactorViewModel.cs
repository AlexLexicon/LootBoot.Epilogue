namespace LootBoot.Epilogue.ViewModels;
public class WaveFactorViewModel : ViewModel, IWaveViewModel
{
    private bool _isComplete;
    public bool IsComplete
    {
        get => _isComplete;
        set => Setter(ref _isComplete, value);
    }
    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set => Setter(ref _isActive, value);
    }
}
