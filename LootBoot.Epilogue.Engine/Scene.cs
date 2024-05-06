namespace LootBoot.Epilogue.Engine;
public abstract class Scene : Script
{
    public Scene(int layer) : base(null, GameSequencer.Permanence.Original)
    {
        Layer = layer;
        GameCore.Scenes.EnqueueCreate(this);
    }
    public int Layer { get; private set; }
}
