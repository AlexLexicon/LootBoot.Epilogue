namespace LootBoot.Epilogue.Engine;
public class Geometry : Script
{
    private delegate void GeometryChangedDelegate(bool isScaleChanged, bool isZScaleChanged, bool isAngleChanged, bool isPositionChanged, bool isSizeChanged);
    public delegate void ChangedDelegate(ChangedEventArgs eventArgs);
    private event GeometryChangedDelegate OnGeometryChange;
    public event ChangedDelegate OnChange;
    public Geometry(Script parent) : this(parent, null) { }
    public Geometry(Script parent, Geometry baseGeometry) : this(parent, baseGeometry, GameSequencer.Permanence.Any) { }
    public Geometry(Script parent, Geometry baseGeometry, GameSequencer.Permanence permanence) : base(parent, permanence)
    {
        BaseGeometry = baseGeometry;
        Angle = new Angle(0);
        Position = new Point(0, 0);
        Size = new Size(0, 0);
    }
    protected override void Update()
    {
        if (EventArgs is not null)
        {
            OnChange?.Invoke(EventArgs);
            EventArgs = null;
        }
        base.Update();
    }
    protected virtual void Change(bool isScaleChanged, bool isZScaleChanged, bool isAngleChanged, bool isPositionChanged, bool isSizeChanged)
    {
        if (EventArgs is null)
        {
            EventArgs = new();
        }
        EventArgs.SetAngle(isAngleChanged);
        EventArgs.SetPosition(isPositionChanged);
        EventArgs.SetSize(isSizeChanged);
        OnGeometryChange?.Invoke(isScaleChanged, isZScaleChanged, isAngleChanged, isPositionChanged, isSizeChanged);
    }
    protected ChangedEventArgs EventArgs { get; set; }
    private Geometry _baseGeometry;
    public Geometry BaseGeometry
    {
        get => _baseGeometry;
        set
        {
            if (_baseGeometry != value)
            {
                if (_baseGeometry is not null)
                {
                    _baseGeometry.OnGeometryChange -= Change;
                }
                _baseGeometry = value;
                if (Angle is not null)
                {
                    Angle.BaseGeometry = _baseGeometry;
                }
                if (Position is not null)
                {
                    Position.BaseGeometry = _baseGeometry;
                }
                if (Size is not null)
                {
                    Size.BaseGeometry = _baseGeometry;
                }
                if (_baseGeometry is not null)
                {
                    _baseGeometry.OnGeometryChange += Change;
                }
            }
        }
    }
    private TGeometricProperty GetGeometricProperty<TGeometricProperty>(ref TGeometricProperty field) where TGeometricProperty : GeometricProperty
    {
        if (field is null)
        {
            ConstructorInfo[] constructor = typeof(TGeometricProperty).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            field = (TGeometricProperty)constructor[0].Invoke(null);
            field.Geometry = this;
            field.BaseGeometry = BaseGeometry;
        }
        return field;
    }
    private void SetGeometricProperty<TGeometricProperty, T>(ref TGeometricProperty field, TGeometricProperty value, Action onChanged) where TGeometricProperty : GeometricProperty<T>
    {
        if (field != value)
        {
            T cachedValue = default;
            bool? isDerivable = null;
            bool? isRotatable = null;
            if (field is not null)
            {
                cachedValue = field.Value;
                isDerivable = field.IsDerivable;
                if (field is GeometricPosition geometricPosition)
                {
                    isRotatable = geometricPosition.IsRotatable;
                }
            }
            field = value;
            if (field is not null)
            {
                field.OnValueChange += onChanged;
                if (isDerivable is not null)
                {
                    field.IsDerivable = isDerivable.Value;
                }
                if (isRotatable is not null && field is GeometricPosition geometricPosition)
                {
                    geometricPosition.IsRotatable = isRotatable.Value;
                }
                field.Geometry = this;
                field.BaseGeometry = BaseGeometry;
                field.CachedValue = cachedValue;
                field.EngineSetter();
            }
        }
    }
    private GeometricAngle _angle;
    public GeometricAngle Angle
    {
        get => _angle;
        set => SetGeometricProperty<GeometricAngle, Angle>(ref _angle, value, AngleChange);
    }
    protected virtual void AngleChange() => Change(false, false, true, false, false);
    private GeometricPosition _position;
    public GeometricPosition Position
    {
        get => _position;
        set => SetGeometricProperty<GeometricPosition, Point>(ref _position, value, PositionChange);
    }
    protected virtual void PositionChange() => Change(false, false, false, true, false);
    private GeometricSize _size;
    public GeometricSize Size
    {
        get => _size;
        set => SetGeometricProperty<GeometricSize, Size>(ref _size, value, SizeChange);
    }
    protected virtual void SizeChange() => Change(false, false, false, false, true);
    private GeometricBounds _bounds;
    public GeometricBounds Bounds => GetGeometricProperty(ref _bounds);
    public class ChangedEventArgs : EventArgs
    {
        public void SetAngle(bool isTrue) => IsAngleChanged = IsAngleChanged || isTrue;
        public void SetPosition(bool isTrue) => IsPositionChanged = IsPositionChanged || isTrue;
        public void SetSize(bool isTrue) => IsSizeChanged = IsSizeChanged || isTrue;
        public bool IsAngleChanged { get; private set; }
        public bool IsPositionChanged { get; private set; }
        public bool IsSizeChanged { get; private set; }
    }
}
