namespace LootBoot.Epilogue.Engine;
public abstract class SpriteAnimation
{
    public enum Directions
    {
        Ascending,
        Descending
    }
    public enum RepeatBehaviors
    {
        Forever,
        Reverse,
        None,
    }
    internal void EngineStart(Sprite sprite) => Start(sprite);
    internal bool EngineStep(Sprite sprite) => Step(sprite);
    protected abstract void Start(Sprite sprite);
    protected abstract bool Step(Sprite sprite);
}
