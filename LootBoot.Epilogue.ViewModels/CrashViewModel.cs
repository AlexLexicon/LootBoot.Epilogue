namespace LootBoot.Epilogue.ViewModels;
public class CrashViewModel : ViewModel
{
    public event Action OnQuit;
    public CrashViewModel(string message)
    {
        ErrorMessage = message;
    }
    public void Quit() => OnQuit?.Invoke();
    public ICommand QuitCommand => new RelayCommand(Quit);
    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => Setter(ref _errorMessage, value);
    }
}
