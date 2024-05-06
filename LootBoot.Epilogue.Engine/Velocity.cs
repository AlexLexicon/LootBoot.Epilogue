namespace LootBoot.Epilogue.Engine;
public class Velocity : Translation<Velocity.State>
{
    public Velocity(Script parent, Geometry geometry) : this(parent, geometry, GameSequencer.Permanence.Any) { }
    public Velocity(Script parent, Geometry geometry, GameSequencer.Permanence permanence) : base(parent, geometry, permanence) => IsMovable = true;
    public override void Set(State state)
    {
        IsMovable = state.IsMoveable;
        MoveTo = state.MoveTo;
        base.Set(state);
    }
    public override State Get() => new()
    {
        Momentum = Momentum,
        Acceleration = Acceleration,
        Throttle = Throttle,
        Maximum = Maximum,
        Value = Value,
        IsMoveable = IsMovable,
        MoveTo = MoveTo,
    };
    public string NAME;
    protected override void Update()
    {
        IsMoveReached = false;
        if (IsMovable)
        {
            if (MoveTo is not null)
            {
                Geometry.MoveToPosition(MoveTo.Value, Value, out bool isReached);
                IsMoveReached = isReached;
            }
            else
            {
                Geometry.MoveAtAngle(Value);
            }
        }
        base.Update();
    }
    public bool IsMovable { get; set; }
    public Point? MoveTo { get; set; }
    public bool IsMoveReached { get; protected set; }
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
            _isMoveable = copyState.IsMoveable;
            MoveTo = copyState.MoveTo;
        }
        public Speed? Momentum { get; init; }
        public Speed? Acceleration { get; init; }
        public double? Throttle { get; init; }
        public Speed? Maximum { get; init; }
        public Speed? Minimum { get; init; }
        public Speed Value { get; init; }
        private bool? _isMoveable;
        public bool IsMoveable
        {
            get => _isMoveable is null || _isMoveable.Value;
            init => _isMoveable = value;
        }
        public Point? MoveTo { get; init; }
    }
}
