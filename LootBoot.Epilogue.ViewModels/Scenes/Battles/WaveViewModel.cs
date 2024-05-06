namespace LootBoot.Epilogue.ViewModels;
public class WaveViewModel : ViewModel, IWaveViewModel
{
    private bool _isDash;
    public bool IsDash
    {
        get => _isDash;
        set => Setter(ref _isDash, value);
    }
    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set => Setter(ref _isActive, value);
    }
    private bool _isComplete;
    public bool IsComplete
    {
        get => _isComplete;
        set => Setter(ref _isComplete, value);
    }
}
