namespace LootBoot.Epilogue.Engine;
public class SpriteAnimator : Script, IEnumerable<Sprite>
{
    public event Action OnAnimationStep;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Sprite> GetEnumerator() => Sprites.GetEnumerator();
    public SpriteAnimator(Script parent) : base(parent)
    {
        Sprites = new HashSet<Sprite>();
    }
    public void Animate(SpriteAnimation animation, bool restart = false)
    {
        if (IsComplete || restart || Animation != animation)
        {
            IsComplete = false;
            Animation = animation;
            if (Animation is not null)
            {
                foreach (Sprite sprite in Sprites)
                {
                    if (sprite is not null && !sprite.IsDestroyed)
                    {
                        Animation.EngineStart(sprite);
                    }
                }
            }
        }
    }
    public void Stop()
    {
        IsComplete = true;
        Animation = null;
    }
    protected override void Update()
    {
        bool isComplete = true;
        if (!IsComplete && Animation is not null)
        {
            foreach (Sprite sprite in Sprites)
            {
                if (sprite is not null)
                {
                    if (!Animation.EngineStep(sprite))
                    {
                        isComplete = false;
                    }
                }
            }
            OnAnimationStep?.Invoke();
        }
        IsComplete = isComplete;
        base.Update();
    }
    public virtual void Add(Sprite sprite)
    {
        if (sprite is not null && !Sprites.Contains(sprite))
        {
            Sprites.Add(sprite);
        }
    }
    public virtual void Add(IEnumerable<Sprite> sprites)
    {
        if (sprites is not null)
        {
            foreach (Sprite sprite in sprites)
            {
                Add(sprite);
            }
        }
    }
    public virtual void Clear() => Sprites.Clear();
    public bool IsComplete { get; protected set; }
    protected HashSet<Sprite> Sprites { get; }
    protected SpriteAnimation Animation { get; private set; }
}
