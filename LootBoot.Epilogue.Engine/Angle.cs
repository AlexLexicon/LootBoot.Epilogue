namespace LootBoot.Epilogue.Engine;
public struct Angle
{
    public Angle(double degrees)
    {
        _degrees = 0;
        _radians = 0;
        Degrees = degrees;
    }
    public Angle Inverse() => new() { Degrees = Degrees + 180 };
    private double _degrees;
    public double Degrees
    {
        get => _degrees;
        set
        {
            if (_degrees != value)
            {
                _degrees = Calculations.Geometry.Angle.Clamp(value);
                _radians = Calculations.Geometry.Angle.ToRadians(_degrees);
            }
        }
    }
    private double _radians;
    public double Radians
    {
        get => _radians;
        set
        {
            if (_radians != value)
            {
                _radians = value;
                _degrees = Calculations.Geometry.Angle.ToDegrees(_radians);
            }
        }
    }
    public override bool Equals(object obj) => obj is Angle b && Degrees == b.Degrees;
    public bool Equals(int value) => Degrees == value;
    public bool Equals(uint value) => Degrees == value;
    public bool Equals(float value) => Degrees == value;
    public bool Equals(double value) => Degrees == value;
    public override int GetHashCode() => (Degrees, Radians).GetHashCode();
    public override string ToString() => Degrees.ToString();
    public static implicit operator int(Angle angle) => (int)angle.Degrees;
    public static implicit operator uint(Angle angle) => (uint)angle.Degrees;
    public static implicit operator float(Angle angle) => (float)angle.Degrees;
    public static implicit operator double(Angle angle) => angle.Degrees;
    public static implicit operator Angle(int value) => new() { Degrees = value };
    public static implicit operator Angle(uint value) => new() { Degrees = value };
    public static implicit operator Angle(float value) => new() { Degrees = value };
    public static implicit operator Angle(double value) => new() { Degrees = value };
    public static Angle operator +(Angle a) => a;
    public static Angle operator +(Angle a, Angle b) => new() { Degrees = a.Degrees + b.Degrees };
    public static Angle operator +(Angle a, int degrees) => new() { Degrees = a.Degrees + degrees };
    public static Angle operator +(Angle a, uint degrees) => new() { Degrees = a.Degrees + degrees };
    public static Angle operator +(Angle a, float degrees) => new() { Degrees = a.Degrees + degrees };
    public static Angle operator +(Angle a, double degrees) => new() { Degrees = a.Degrees + degrees };
    public static Angle operator +(int degrees, Angle a) => new() { Degrees = degrees + a.Degrees };
    public static Angle operator +(uint degrees, Angle a) => new() { Degrees = degrees + a.Degrees };
    public static Angle operator +(float degrees, Angle a) => new() { Degrees = degrees + a.Degrees };
    public static Angle operator +(double degrees, Angle a) => new() { Degrees = degrees + a.Degrees };
    public static Angle operator -(Angle a) => new() { Degrees = -a };
    public static Angle operator -(Angle a, Angle b) => new() { Degrees = a.Degrees - b.Degrees };
    public static Angle operator -(Angle a, int degrees) => new() { Degrees = a.Degrees - degrees };
    public static Angle operator -(Angle a, uint degrees) => new() { Degrees = a.Degrees - degrees };
    public static Angle operator -(Angle a, float degrees) => new() { Degrees = a.Degrees - degrees };
    public static Angle operator -(Angle a, double degrees) => new() { Degrees = a.Degrees - degrees };
    public static Angle operator -(int degrees, Angle a) => new() { Degrees = degrees - a.Degrees };
    public static Angle operator -(uint degrees, Angle a) => new() { Degrees = degrees - a.Degrees };
    public static Angle operator -(float degrees, Angle a) => new() { Degrees = degrees - a.Degrees };
    public static Angle operator -(double degrees, Angle a) => new() { Degrees = degrees - a.Degrees };
    public static bool operator ==(Angle a, Angle b) => a.Equals(b);
    public static bool operator ==(Angle a, int degrees) => a.Equals(degrees);
    public static bool operator ==(Angle a, uint degrees) => a.Equals(degrees);
    public static bool operator ==(Angle a, float degrees) => a.Equals(degrees);
    public static bool operator ==(Angle a, double degrees) => a.Equals(degrees);
    public static bool operator ==(int degrees, Angle a) => a.Equals(degrees);
    public static bool operator ==(uint degrees, Angle a) => a.Equals(degrees);
    public static bool operator ==(float degrees, Angle a) => a.Equals(degrees);
    public static bool operator ==(double degrees, Angle a) => a.Equals(degrees);
    public static bool operator !=(Angle a, Angle b) => !(a == b);
    public static bool operator !=(Angle a, int degrees) => !(a == degrees);
    public static bool operator !=(Angle a, uint degrees) => !(a == degrees);
    public static bool operator !=(Angle a, float degrees) => !(a == degrees);
    public static bool operator !=(Angle a, double degrees) => !(a == degrees);
    public static bool operator !=(int degrees, Angle a) => !(degrees == a);
    public static bool operator !=(uint degrees, Angle a) => !(degrees == a);
    public static bool operator !=(float degrees, Angle a) => !(degrees == a);
    public static bool operator !=(double degrees, Angle a) => !(degrees == a);
    public static bool operator >(Angle a, Angle b) => a.Degrees > b.Degrees;
    public static bool operator >(Angle a, int degrees) => a.Degrees > degrees;
    public static bool operator >(Angle a, uint degrees) => a.Degrees > degrees;
    public static bool operator >(Angle a, float degrees) => a.Degrees > degrees;
    public static bool operator >(Angle a, double degrees) => a.Degrees > degrees;
    public static bool operator >(int degrees, Angle a) => degrees > a.Degrees;
    public static bool operator >(uint degrees, Angle a) => degrees > a.Degrees;
    public static bool operator >(float degrees, Angle a) => degrees > a.Degrees;
    public static bool operator >(double degrees, Angle a) => degrees > a.Degrees;
    public static bool operator >=(Angle a, Angle b) => a.Degrees >= b.Degrees;
    public static bool operator >=(Angle a, int degrees) => a.Degrees >= degrees;
    public static bool operator >=(Angle a, uint degrees) => a.Degrees >= degrees;
    public static bool operator >=(Angle a, float degrees) => a.Degrees >= degrees;
    public static bool operator >=(Angle a, double degrees) => a.Degrees >= degrees;
    public static bool operator >=(int degrees, Angle a) => degrees >= a.Degrees;
    public static bool operator >=(uint degrees, Angle a) => degrees >= a.Degrees;
    public static bool operator >=(float degrees, Angle a) => degrees >= a.Degrees;
    public static bool operator >=(double degrees, Angle a) => degrees >= a.Degrees;
    public static bool operator <(Angle a, Angle b) => a.Degrees < b.Degrees;
    public static bool operator <(Angle a, int degrees) => a.Degrees < degrees;
    public static bool operator <(Angle a, uint degrees) => a.Degrees < degrees;
    public static bool operator <(Angle a, float degrees) => a.Degrees < degrees;
    public static bool operator <(Angle a, double degrees) => a.Degrees < degrees;
    public static bool operator <(int degrees, Angle a) => degrees < a.Degrees;
    public static bool operator <(uint degrees, Angle a) => degrees < a.Degrees;
    public static bool operator <(float degrees, Angle a) => degrees < a.Degrees;
    public static bool operator <(double degrees, Angle a) => degrees < a.Degrees;
    public static bool operator <=(Angle a, Angle b) => a.Degrees <= b.Degrees;
    public static bool operator <=(Angle a, int degrees) => a.Degrees <= degrees;
    public static bool operator <=(Angle a, uint degrees) => a.Degrees <= degrees;
    public static bool operator <=(Angle a, float degrees) => a.Degrees <= degrees;
    public static bool operator <=(Angle a, double degrees) => a.Degrees <= degrees;
    public static bool operator <=(int degrees, Angle a) => degrees <= a.Degrees;
    public static bool operator <=(uint degrees, Angle a) => degrees <= a.Degrees;
    public static bool operator <=(float degrees, Angle a) => degrees <= a.Degrees;
    public static bool operator <=(double degrees, Angle a) => degrees <= a.Degrees;
}
