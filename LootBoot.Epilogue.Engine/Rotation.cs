namespace LootBoot.Epilogue.Engine;
public class Rotation : Translation<Rotation.State>
{
    public Rotation(Script parent, Geometry geometry) : this(parent, geometry, GameSequencer.Permanence.Any) { }
    public Rotation(Script parent, Geometry geometry, GameSequencer.Permanence permanence) : base(parent, geometry, permanence) => IsRotatable = true;
    public override void Set(State state)
    {
        IsRotatable = state.IsRotatable;
        RotateTo = state.RotateTo;
        AngleTo = state.AngleTo;
        base.Set(state);
    }
    public override State Get() => new()
    {
        Momentum = Momentum,
        Acceleration = Acceleration,
        Throttle = Throttle,
        Maximum = Maximum,
        Value = Value,
        IsRotatable = IsRotatable,
        RotateTo = RotateTo,
        AngleTo = AngleTo,
    };
    protected override void Update()
    {
        IsRotateReached = false;
        if (IsRotatable)
        {
            if (RotateTo is not null)
            {
                Geometry.RotateToPosition(RotateTo.Value, Value, out bool isReached);
                IsRotateReached = isReached;
            }
            else if (AngleTo is not null)
            {
                Geometry.RotateToAngle(AngleTo.Value, Value, out bool isReached);
                IsRotateReached = isReached;
            }
            else
            {
                Geometry.Angle.Value += Value.Distance;
            }
        }
        base.Update();
    }
    public bool IsRotatable { get; set; }
    public Point? RotateTo { get; set; }
    public Angle? AngleTo { get; set; }
    public bool IsRotateReached { get; protected set; }
    public struct State : IState
    {
        public State(State copyState)
        {
            Momentum = copyState.Momentum;
            Acceleration = copyState.Acceleration;
            Throttle = copyState.Throttle;
            Maximum = copyState.Maximum;
            Minimum = copyState.Minimum;
            Value = copyState.Value;
            _isRotatable = copyState.IsRotatable;
            RotateTo = copyState.RotateTo;
            AngleTo = copyState.AngleTo;
        }
        public Speed? Momentum { get; init; }
        public Speed? Acceleration { get; init; }
        public double? Throttle { get; init; }
        public Speed? Maximum { get; init; }
        public Speed? Minimum { get; init; }
        public Speed Value { get; init; }
        private bool? _isRotatable;
        public bool IsRotatable
        {
            get => _isRotatable is null || _isRotatable.Value;
            init => _isRotatable = value;
        }
        public Point? RotateTo { get; init; }
        public Angle? AngleTo { get; init; }
    }
}
