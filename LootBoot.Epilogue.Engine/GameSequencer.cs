namespace LootBoot.Epilogue.Engine;
public class GameSequencer<TGameObject> : GameSequencer, IEnumerable<TGameObject> where TGameObject : GameObject
{
    public event Action OnChanged;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<TGameObject> GetEnumerator() => GameObjects.GetEnumerator();
    public GameSequencer()
    {
        GameObjects = new HashSet<TGameObject>();
        CreateQueue = new Queue<TGameObject>();
        DestroyQueue = new Queue<TGameObject>();
    }
    public void EnqueueCreate(TGameObject gameObject)
    {
        if (gameObject is not null && !CreateQueue.Contains(gameObject))
        {
            gameObject.Sequencer = this;
            CreateQueue.Enqueue(gameObject);
        }
    }
    public void DequeueDestroy(TGameObject gameObject)
    {
        if (gameObject is not null && !DestroyQueue.Contains(gameObject))
        {
            gameObject.Sequencer = this;
            DestroyQueue.Enqueue(gameObject);
        }
    }
    public void Clear()
    {
        if (IsFrontUpdating || IsBackUpdating)
        {
            IsSuspendClear = true;
        }
        else
        {
            Clear(true);
        }
    }
    protected void Changed() => OnChanged?.Invoke();
    internal override void Dequeue(GameObject gameObject) => DequeueDestroy((TGameObject)gameObject);
    private void Clear(bool isClear)
    {
        if (isClear)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                Destroy(gameObject);
            }
            CreateQueue.Clear();
            DestroyQueue.Clear();
            GameObjects.Clear();
        }
    }
    public void UpdateFront()
    {
        IsFrontUpdating = true;
        HashSet<TGameObject> requeue = new HashSet<TGameObject>();
        while (DestroyQueue.Count > 0)
        {
            TGameObject gameObject = DestroyQueue.Dequeue();
            if (gameObject is not null)
            {
                if (gameObject.EngineCanDestroy())
                {
                    if (GameObjects.Contains(gameObject))
                    {
                        Remove(gameObject);
                        Changed();
                    }
                    Destroy(gameObject);
                }
                else
                {
                    requeue.Add(gameObject);
                }
            }
        }
        if (requeue.Count > 0)
        {
            foreach (TGameObject gameObject in requeue)
            {
                DequeueDestroy(gameObject);
            }
        }
        foreach (GameObject gameObject in GameObjects)
        {
            if (gameObject.IsCreated && !gameObject.IsDestroyed && gameObject.IsUpdatable)
            {
                gameObject.EngineUpdate();
            }
        }
        IsFrontUpdating = false;
        Clear(IsSuspendClear);
    }
    public void UpdateBack()
    {
        IsBackUpdating = true;
        while (CreateQueue.Count > 0)
        {
            TGameObject gameObject = CreateQueue.Dequeue();
            if (gameObject is not null && !gameObject.IsCreated && !gameObject.IsDestroyed && !GameObjects.Contains(gameObject) && IsAddable(gameObject))
            {
                Add(gameObject);
                gameObject.EngineCreate();
                Changed();
            }
        }
        IsBackUpdating = false;
        Clear(IsSuspendClear);
    }
    protected virtual bool IsAddable(TGameObject gameObject, bool isAddable = true)
    {
        if (gameObject.Permanence == Permanence.Newest)
        {
            foreach (TGameObject originalGameObject in GetGameObjectsOfType(gameObject.GetType()))
            {
                Dequeue(originalGameObject);
            }
        }
        else if (gameObject.Permanence == Permanence.Original && GetGameObjectsOfType(gameObject.GetType()).Count > 0)
        {
            isAddable = false;
        }
        return isAddable;
    }
    protected virtual void Add(TGameObject gameObject)
    {
        GameObjects.Add(gameObject);
        gameObject.Sequencer = this;
    }
    protected virtual void Remove(TGameObject gameObject)
    {
        GameObjects.Remove(gameObject);
        gameObject.Sequencer = null;
    }
    private void Destroy(GameObject gameObject)
    {
        if (gameObject is not null && !gameObject.IsDestroyed)
        {
            gameObject.EngineDestroy();
        }
    }
    private List<TGameObject> GetGameObjectsOfType(Type type) => GameObjects.Where(gameObject => Reflecting.IsOrDerivedFrom(gameObject?.GetType(), type) || Reflecting.IsOrDerivedFrom(type, gameObject.GetType())).ToList();
    internal override int Count => GameObjects.Count;
    internal HashSet<TGameObject> GameObjects { get; set; }
    internal override HashSet<GameObject> Collection => GameObjects.Select(go => (GameObject)go).ToHashSet();
    internal Queue<TGameObject> CreateQueue { get; set; }
    internal Queue<TGameObject> DestroyQueue { get; set; }
    private bool IsFrontUpdating { get; set; }
    private bool IsBackUpdating { get; set; }
    private bool IsSuspendClear { get; set; }
}
public abstract class GameSequencer
{
    public enum Permanence
    {
        Any,
        Original,
        Newest,
    }
    internal abstract HashSet<GameObject> Collection { get; }
    internal abstract void Dequeue(GameObject gameObject);
    internal abstract int Count { get; }
}
