namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones.Enter;
public class ZoneEnterWaitBehavior : ZoneEnterBehavior
{
    private const double ENTER_DURATION_SECONDS = 2;
    private DateTime? enterTime;
    protected override void Create()
    {
        base.Create();
        Source.Fuselage.Sprites.SetSizeScale(2);
        Source.Weapon.Sprites.SetSizeScale(2);
        Viewport.Watched.Set(Geometry);
    }
    protected override void Update()
    {
        if (Calculations.DateTime.IsSeconds(ref enterTime, ENTER_DURATION_SECONDS))
        {
            Complete();
        }
        base.Update();
    }
}
