namespace LootBoot.Epilogue.Engine;
public class Behavior<TSourceScript> : Behavior where TSourceScript : Script
{
    public new TSourceScript Source
    {
        get
        {
            try
            {
                return (TSourceScript)base.Source;
            }
            catch
            {
                Type type = typeof(TSourceScript);
                throw new Exception($"The source script type {base.Source?.GetType()?.Name ?? "null"} is not of the type {type.Name}");
            }
        }
    }
}
public class Behavior : ScriptCollection
{
    public event Action OnStart;
    public event Action OnComplete;
    public Behavior() : base(GameSequencer.Permanence.Any) { }
    //public void Start() => Start();
    //public void Complete() => Complete();
    public virtual void Start()
    {
        IsComplete = false;
        OnStart?.Invoke();
    }
    public virtual void Complete()
    {
        IsComplete = true;
        OnComplete?.Invoke();
    }
    public bool IsComplete { get; protected set; }
}
