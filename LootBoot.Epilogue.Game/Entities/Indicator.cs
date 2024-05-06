namespace LootBoot.Epilogue.Game.Entities;
public abstract class Indicator : Entity
{
    protected Indicator(Script parent) : this(parent, null) { }
    protected Indicator(Script parent, Geometry baseGeometry) : base(parent, baseGeometry) { }
}
