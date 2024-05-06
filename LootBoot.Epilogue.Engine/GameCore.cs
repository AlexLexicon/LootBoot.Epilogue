namespace LootBoot.Epilogue.Engine;
public static class GameCore
{
    public delegate void UpdateException(Exception exception);
    public static event UpdateException OnUpdateException;
    public static event Action OnTick;
    public static event Action OnUpdate;
    private static DateTime lastUpdated;
    public static void Start()
    {
        Scenes = new SceneSequencer();
        CoreLoop.Start();
    }
    private static void Update()
    {
        SecondDelta = (DateTime.Now - lastUpdated).TotalSeconds;
        Input.Update();
        OnTick?.Invoke();
        if (!IsPaused)
        {
            try
            {
                Scenes.UpdateFront();
            }
            catch (TaskCanceledException) { }
            catch (Exception exception)
            {
                GameConsole.Write($"Scenes.UpdateFront: {exception.Message}");
                OnUpdateException?.Invoke(exception);
            }
            try
            {
                Viewport.Update();
            }
            catch (TaskCanceledException) { }
            catch (Exception exception)
            {
                GameConsole.Write($"Viewport.Update: {exception.Message}");
                OnUpdateException?.Invoke(exception);
            }
            try
            {
                OnUpdate?.Invoke();
            }
            catch (TaskCanceledException) { }
            catch (Exception exception)
            {
                GameConsole.Write($"Updated?.Invoke: {exception.Message}");
                OnUpdateException?.Invoke(exception);
            }
            try
            {
                Scenes.UpdateBack();
            }
            catch (TaskCanceledException) { }
            catch (Exception exception)
            {
                GameConsole.Write($"Scenes.UpdateBack: {exception.Message}");
                OnUpdateException?.Invoke(exception);
            }
            try
            {
                Viewport.Draw.Update();
            }
            catch (TaskCanceledException) { }
            catch (Exception exception)
            {
                GameConsole.Write($"Viewport.Draw.Update: {exception.Message}");
                OnUpdateException?.Invoke(exception);
            }
        }
        Tick++;
        lastUpdated = DateTime.Now;
    }
    public static void Stop() => CoreLoop.Stop();
    public static bool IsPaused { get; set; }
    public static int Tick { get; private set; }
    public static double SecondDelta { get; private set; }
    internal static SceneSequencer Scenes { get; private set; }
    private static GameCoreLoop _coreLoop;
    private static GameCoreLoop CoreLoop => _coreLoop ??= new(Update);
    private class GameCoreLoop
    {
        private const double UPDATE_INTERVAL_MILLISECONDS = 16;
        private TimeSpan interval;
        public GameCoreLoop(Action updateAction) => UpdateAction = updateAction;
        public void Start()
        {
            interval = TimeSpan.FromMilliseconds(UPDATE_INTERVAL_MILLISECONDS);
            CancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(GameLoop, CancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);
        }
        private void GameLoop()
        {
            while (true)
            {
                try
                {
                    UpdateAction?.Invoke();
                }
                catch (Exception exception)
                {
                    GameConsole.Write($"UpdateAction?.Invoke: {exception.Message}");
                    return;
                }
                if (CancellationTokenSource.Token.WaitHandle.WaitOne(interval))
                {
                    break;
                }
            }
        }
        public void Stop() => CancellationTokenSource.Cancel();
        private Action UpdateAction { get; set; }
        private CancellationTokenSource CancellationTokenSource { get; set; }
    }
    internal class SceneSequencer : GameSequencer<Scene>
    {
        protected override bool IsAddable(Scene gameObject, bool isAddable = true)
        {
            foreach (Scene scene in GameObjects.Where(s => s.Layer == gameObject.Layer))
            {
                Dequeue(scene);
            }
            return base.IsAddable(gameObject, isAddable);
        }
    }
}
