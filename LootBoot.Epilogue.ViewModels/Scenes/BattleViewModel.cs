namespace LootBoot.Epilogue.ViewModels;
public class BattleViewModel : SceneViewModel<Battle>
{
    public BattleViewModel(Battle scene) : base(scene)
    {
        scene.OnWavesChange += WavesChange;
        scene.OnWaveFactorsChange += WaveFactorsChange;
        scene.OnStart += () =>
        {
            IsStarted = true;
            IsEnemiesIncoming = true;
        };
        scene.OnWaveComplete += () =>
        {
            IsWaveCompelete = true;
            IsEnemiesIncoming = true;
        };
        scene.OnWaveStart += () =>
        {
            IsCompletable = true;
            IsWaveCompelete = false;
            IsEnemiesIncoming = false;
        };
        scene.OnComplete += () =>
        {
            IsCreated = false;
        };
        IsWavesLeft = false;
        IsEnemiesIncoming = false;
    }
    private void WavesChange(int totalWaves, int remainingWaves)
    {
        WaveViewModels = Refresh(totalWaves, remainingWaves, WaveViewModels, (isLast) => new WaveViewModel { IsDash = !isLast, });
        IsWavesLeft = remainingWaves > 0;
        IsCreated = true;
    }
    private void WaveFactorsChange(int totalFactors, int remainingFactors)
    {
        WaveFactorViewModels = Refresh(totalFactors, remainingFactors, WaveFactorViewModels, (isLast) => new WaveFactorViewModel());
        IsWaveFactors = WaveFactorViewModels is not null && WaveFactorViewModels.Count > 0;
    }
    private ObservableCollection<TWaveViewModel> Refresh<TWaveViewModel>(int total, int remaining, ObservableCollection<TWaveViewModel> collection, Func<bool, TWaveViewModel> getNewItem) where TWaveViewModel : IWaveViewModel
    {
        if (collection is null || total != collection.Count)
        {
            collection = new();
            for (int count = 0; count < total; count++)
            {
                collection.Add(getNewItem.Invoke(count == total - 1));
            }
        }
        if (collection is not null)
        {
            int inCompletedIndex = total - remaining;
            for (int index = 0; index < collection.Count; index++)
            {
                collection[index].IsActive = false;
                collection[index].IsComplete = index < inCompletedIndex;
                if (index == inCompletedIndex)
                {
                    collection[index].IsActive = true;
                }
            }
        }
        return collection;
    }
    private ObservableCollection<WaveViewModel> _waveViewModels;
    public ObservableCollection<WaveViewModel> WaveViewModels
    {
        get => _waveViewModels;
        set => Setter(ref _waveViewModels, value);
    }
    private ObservableCollection<WaveFactorViewModel> _waveFactorViewModels;
    public ObservableCollection<WaveFactorViewModel> WaveFactorViewModels
    {
        get => _waveFactorViewModels;
        set => Setter(ref _waveFactorViewModels, value);
    }
    private bool _isCreated;
    public bool IsCreated
    {
        get => _isCreated;
        set => Setter(ref _isCreated, value);
    }
    private bool _isWaveComplete;
    public bool IsWaveCompelete
    {
        get => _isWaveComplete;
        set => Setter(ref _isWaveComplete, value);
    }
    private bool _isWaveFactors;
    public bool IsWaveFactors
    {
        get => _isWaveFactors;
        set => Setter(ref _isWaveFactors, value);
    }
    private bool _isCompletable;
    public bool IsCompletable
    {
        get => _isCompletable;
        set => Setter(ref _isCompletable, value);
    }
    private bool _isEnemiesIncoming;
    public bool IsEnemiesIncoming
    {
        get => _isEnemiesIncoming;
        set => Setter(ref _isEnemiesIncoming, value);
    }
    private bool _isWavesLeft;
    public bool IsWavesLeft
    {
        get => _isWavesLeft;
        set => Setter(ref _isWavesLeft, value);
    }
    private bool _isStarted;
    public bool IsStarted
    {
        get => _isStarted;
        set => Setter(ref _isStarted, value);
    }
}
