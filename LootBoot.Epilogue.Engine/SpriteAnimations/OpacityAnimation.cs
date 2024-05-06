namespace LootBoot.Epilogue.Engine.SpriteAnimations;
public class OpacityAnimation : SpriteAnimation
{
    public const double SPEED_DEFAULT = 0.05;
    private const double OPACITY_MINIMUM = 0;
    private const double OPACITY_MAXIMUM = 1;
    public OpacityAnimation() : this(OPACITY_MINIMUM, OPACITY_MAXIMUM, SPEED_DEFAULT) { }
    public OpacityAnimation(double? from, double to) : this(from, to, SPEED_DEFAULT) { }
    public OpacityAnimation(double? from, double to, double speed)
    {
        From = from;
        To = to;
        Speed = speed;
    }
    protected override void Start(Sprite sprite)
    {
        if (From is not null)
        {
            sprite.Opacity = From.Value;
        }
    }
    protected override bool Step(Sprite sprite)
    {
        double opacity = sprite.Opacity;
        bool isComplete = opacity == To;
        if (!isComplete)
        {
            if (opacity < To)
            {
                opacity += Speed;
                if (opacity > To)
                {
                    opacity = To;
                    isComplete = true;
                }
            }
            else if (opacity > To)
            {
                opacity -= Speed;
                if (opacity < To)
                {
                    opacity = To;
                    isComplete = true;
                }
            }
            sprite.Opacity = opacity;
        }
        return isComplete;
    }
    private double? _from;
    public double? From
    {
        get => _from;
        set
        {
            if (_from != value)
            {
                _from = value is not null ? Math.Clamp(value.Value, OPACITY_MINIMUM, OPACITY_MAXIMUM) : value;
            }
        }
    }
    private double _to;
    public double To
    {
        get => _to;
        set
        {
            if (_to != value)
            {
                _to = Math.Clamp(value, OPACITY_MINIMUM, OPACITY_MAXIMUM);
            }
        }
    }
    private double _speed;
    public double Speed
    {
        get => _speed;
        set
        {
            if (_speed != value)
            {
                _speed = value;
                if (_speed < 0)
                {
                    throw new Exception("Speed cannot be a negative number");
                }
            }
        }
    }
}
