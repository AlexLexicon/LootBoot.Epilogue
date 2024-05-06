namespace LootBoot.Epilogue.Game.Behaviors;
public class SpacecraftBehavior : Behavior<Spacecraft>
{
    //protected override void OnCreate()
    //{
    //    base.OnCreate();
    //    Fuselage = 
    //    Weapon = GetScript<Weapon>();
    //}
    //protected override void OnUpdate()
    //{
    //    Fuselage = GetScript<Fuselage>();
    //    Weapon = 
    //    base.OnUpdate();
    //}
    protected Fuselage Fuselage => GetScript<Fuselage>();
    protected Weapon Weapon => GetScript<Weapon>();
    protected Geometry Geometry => Fuselage.Geometry;
    protected Velocity Velocity => Fuselage.Velocity;
    protected Rotation Rotation => Fuselage.Rotation;
    protected Defense Defense => Fuselage.Defense;
    protected IntegrityIndicator IntegrityIndicator => Fuselage.IntegrityIndicator;
}
