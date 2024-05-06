namespace LootBoot.Epilogue.Engine;
public abstract class Translation<TState> : Script where TState : struct, Translation<TState>.IState
{
    public Translation(Script parent, Geometry geometry) : this(parent, geometry, GameSequencer.Permanence.Any) { }
    public Translation(Script parent, Geometry geometry, GameSequencer.Permanence permanence) : base(parent, permanence) => Geometry = geometry;
    public virtual void Set(TState state)
    {
        Momentum = state.Momentum;
        Acceleration = state.Acceleration;
        Throttle = state.Throttle;
        Maximum = state.Maximum;
        Minimum = state.Minimum;
        Value = state.Value;
    }
    public abstract TState Get();
    protected override void Update()
    {
        double throttle = Throttle is not null ? Throttle.Value : 0;
        bool isAccelerating = throttle != 0;
        Speed? max = null;
        if (Maximum is not null)
        {
            max = _throttle is not null && _throttle.Value > 0 ? Calculations.Percentage.Of(_throttle.Value, Maximum.Value) : Maximum;
        }
        if (max is not null && Value > max.Value)
        {
            Value = Math.Min(max.Value, Value);
            isAccelerating = false;
        }
        Speed? min = null;
        if (Minimum is not null)
        {
            min = _throttle is not null && _throttle.Value < 0 ? -Calculations.Percentage.Of(_throttle.Value, Minimum.Value) : Minimum;
        }
        if (min is not null && Value < min.Value)
        {
            Value = Math.Max(min.Value, Value);
            isAccelerating = false;
        }
        if (isAccelerating)
        {
            Speed acceleration = Acceleration is null ? 0 : Acceleration.Value;
            Speed momentum = Momentum is null ? 0 : Momentum.Value;
            if (throttle > 0)
            {
                if (Value >= 0)
                {
                    Value += acceleration;
                }
                else
                {
                    Value += acceleration + momentum;
                }
            }
            else
            {
                if (Value <= 0)
                {
                    Value -= acceleration;
                }
                else
                {
                    Value -= acceleration + momentum;
                }
            }
        }
        if (!isAccelerating && Momentum is not null && Value != 0)
        {
            Value = Value > 0 ? Math.Max(Value - Momentum.Value, 0) : Math.Min(Value + Momentum.Value, 0);
        }
    }
    protected Geometry Geometry { get; }
    private Speed? _momentum;
    public Speed? Momentum
    {
        get => _momentum;
        set
        {
            _momentum = value;
            if (_momentum is not null && _momentum.Value < 0)
            {
                _momentum = Math.Abs(_momentum.Value);
            }
        }
    }
    private Speed? _acceleration;
    public Speed? Acceleration
    {
        get => _acceleration;
        set
        {
            _acceleration = value;
            if (_acceleration is not null && _acceleration.Value < 0)
            {
                _acceleration = Math.Abs(_acceleration.Value);
            }
        }
    }
    private double? _throttle;
    public double? Throttle
    {
        get => _throttle;
        set => _throttle = value is null ? value : Math.Clamp(value.Value, -1, 1);
    }
    public Speed? Maximum { get; set; }
    public Speed? Minimum { get; set; }
    public Speed Value { get; set; }
    public interface IState
    {
        public Speed? Momentum { get; init; }
        public Speed? Acceleration { get; init; }
        public double? Throttle { get; init; }
        public Speed? Maximum { get; init; }
        public Speed? Minimum { get; init; }
        public Speed Value { get; init; }
    }
}
