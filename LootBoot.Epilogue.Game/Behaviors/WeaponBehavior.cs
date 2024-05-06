namespace LootBoot.Epilogue.Game.Behaviors;
public abstract class WeaponBehavior<TWeapon> : Behavior<TWeapon> where TWeapon : Weapon
{
    private DateTime? lastActivatedTime;
    protected override void Create()
    {
        Geometry = GetScript<Geometry>();
        Scene = GetParent<Scene>();
        base.Create();
    }
    protected override void Update()
    {
        if (Source.IsActivated)
        {
            if (Calculations.DateTime.IsSeconds(ref lastActivatedTime, Source.Attributes.FireRate))
            {
                lastActivatedTime = null;
                Activate();
            }
        }
        base.Update();
    }
    protected abstract void Activate();
    protected Geometry Geometry { get; private set; }
    protected Scene Scene { get; private set; }
}
