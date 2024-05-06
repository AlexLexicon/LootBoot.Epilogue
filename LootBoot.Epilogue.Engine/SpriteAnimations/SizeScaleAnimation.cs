namespace LootBoot.Epilogue.Engine.SpriteAnimations;
public class SizeScaleAnimation : SpriteAnimation
{
    public const double SPEED_DEFAULT = 0.05;
    private const double SCALE_MINIMUM = 0;
    private const double SCALE_MAXIMUM = int.MaxValue;
    public SizeScaleAnimation() : this(SCALE_MINIMUM, SCALE_MAXIMUM, SPEED_DEFAULT) { }
    public SizeScaleAnimation(double? from, double to) : this(from, to, SPEED_DEFAULT) { }
    public SizeScaleAnimation(double? from, double to, double speed)
    {
        From = from;
        To = to;
        Speed = speed;
    }
    protected override void Start(Sprite sprite)
    {
        if (From is not null)
        {
            sprite.SizeScale = From.Value;
        }
    }
    protected override bool Step(Sprite sprite)
    {
        double scale = sprite.SizeScale;
        bool isComplete = scale == To;
        if (!isComplete)
        {
            if (scale < To)
            {
                scale += Speed;
                if (scale > To)
                {
                    scale = To;
                    isComplete = true;
                }
            }
            else if (scale > To)
            {
                scale -= Speed;
                if (scale < To)
                {
                    scale = To;
                    isComplete = true;
                }
            }
            sprite.SizeScale = scale;
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
                _from = value is not null ? Math.Clamp(value.Value, SCALE_MINIMUM, SCALE_MAXIMUM) : value;
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
                _to = Math.Clamp(value, SCALE_MINIMUM, SCALE_MAXIMUM);
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
