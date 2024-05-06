namespace LootBoot.Epilogue.Engine;
public static class GameConsole
{
    public static event Action<string> OnCommand;
    public static void Log(string className, object details = null, [CallerMemberName] string memberName = null) => Write((className ?? "null") + '.' + (memberName ?? "null") + ' ' + (details?.ToString() ?? string.Empty));
    public static void Write(string message)
    {
        string text = DateTime.Now.ToString("[MM-dd-yyyy:HH.dd.ss.fff][" + GameCore.Tick.ToString() + "]: ") + (message ?? "null");
        Console?.Write(text);
        MessagesBuffer.Add(text);
    }
    public static void Command(string message)
    {
        Write(message);
        OnCommand?.Invoke(message);
    }
    public static void Monitor(object obj) => Console?.Monitor(obj);
    public static List<string> Messages => Console?.Messages;
    private static List<string> MessagesBuffer { get; set; } = new List<string>();
    private static IGameConsole _console;
    public static IGameConsole Console
    {
        get => _console;
        set
        {
            if (_console != value)
            {
                _console = value;
                if (_console is not null)
                {
                    foreach (string message in MessagesBuffer)
                    {
                        _console.Write(message);
                    }
                }
            }
        }
    }
    public interface IGameConsole
    {
        void Write(string message);
        void Monitor(object obj);
        List<string> Messages { get; }
    }
}
