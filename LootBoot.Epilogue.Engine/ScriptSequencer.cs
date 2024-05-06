namespace LootBoot.Epilogue.Engine;
public abstract class ScriptSequencer : ScriptCollection
{
    public ScriptSequencer(GameSequencer.Permanence permanence) : base(permanence) => Scripts = new GameSequencer<Script>();
    protected override void Destroy()
    {
        if (Scripts is not null)
        {
            Scripts.Clear();
        }
        base.Destroy();
    }
    internal override void Add(Script script)
    {
        if (Scripts is not null)
        {
            Scripts.EnqueueCreate(script);
        }
        base.Add(script);
    }
    internal override void EngineUpdate()
    {
        if (Scripts is not null)
        {
            Scripts.UpdateFront();
            Update();
            Scripts.UpdateBack();
        }
    }
    internal GameSequencer<Script> Scripts { get; }
}
