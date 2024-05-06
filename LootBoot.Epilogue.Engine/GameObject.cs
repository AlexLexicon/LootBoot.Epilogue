namespace LootBoot.Epilogue.Engine;
public abstract class GameObject
{
    public event Action OnCreate;
    public event Action OnDestroy;
    public GameObject(GameSequencer.Permanence permanence)
    {
        Permanence = permanence;
        IsUpdatable = true;
    }
    public virtual void QueueDestroy()
    {
        if (Sequencer is not null)
        {
            Sequencer.Dequeue(this);
        }
    }
    protected virtual void Create()
    {
        IsCreated = true;
        OnCreate?.Invoke();
    }
    protected virtual void Update() { }
    protected virtual bool CanDestroy() => true;
    protected virtual void Destroy()
    {
        IsDestroyed = true;
        OnDestroy?.Invoke();
    }
    internal virtual void EngineCreate() => Create();
    internal virtual void EngineUpdate() => Update();
    internal virtual void EngineDestroy() => Destroy();
    internal virtual bool EngineCanDestroy() => CanDestroy();
    internal GameSequencer.Permanence Permanence { get; }
    public virtual bool IsUpdatable { get; set; }
    public bool IsCreated { get; private set; }
    public bool IsDestroyed { get; private set; }
    internal GameSequencer Sequencer { get; set; }
}
