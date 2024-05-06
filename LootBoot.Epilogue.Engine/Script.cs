namespace LootBoot.Epilogue.Engine;
public abstract class Script : ScriptSequencer
{
    public Script(Script parent) : this(parent, GameSequencer.Permanence.Any) { }
    public Script(Script parent, GameSequencer.Permanence permanence) : base(permanence)
    {
        Parent = parent;
        Source = this;
        if (parent is not null)
        {
            parent.Add(this);
        }
    }
    public virtual Script Parent { get; }
}
