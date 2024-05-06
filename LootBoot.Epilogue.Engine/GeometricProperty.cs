namespace LootBoot.Epilogue.Engine;
public abstract class GeometricProperty<T> : GeometricProperty
{
    internal GeometricProperty() : base() { }
    internal void EngineSetter()
    {
        if (!Value.Equals(CachedValue))
        {
            ValueChange();
        }
    }
    protected abstract T GetDerivedValue();
    protected abstract T GetScreenValue();
    internal T CachedValue { get; set; }
    private T _value;
    public virtual T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value))
            {
                _value = value;
                ValueChange();
            }
        }
    }
    public T World => IsDerivable ? Derived : Value;
    public T Derived => GetDerivedValue();
    public T Screen => GetScreenValue();
}
public abstract class GeometricProperty
{
    public event Action OnValueChange;
    internal GeometricProperty() => IsDerivable = true;
    internal virtual void ValueChange() => OnValueChange?.Invoke();
    internal Geometry BaseGeometry { get; set; }
    internal Geometry Geometry { get; set; }
    public bool IsDerivable { get; set; }
}
