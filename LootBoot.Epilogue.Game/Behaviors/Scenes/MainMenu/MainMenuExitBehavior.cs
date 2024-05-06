namespace LootBoot.Epilogue.Game.Behaviors.Scenes.MainMenu;
public class MainMenuExitBehavior : MainMenuBehavior
{
    protected const double VIEWPORT_VELOCITY = 500;
    protected const double SPACECRAFT_VELOCITY_EXIT = 2000;
    protected override void Create()
    {
        base.Create();
        Velocity.Set(new()
        {
            Value = SPACECRAFT_VELOCITY_EXIT,
        });
        Viewport.Watched.Set(null);
        Viewport.Geometry.Angle = GEOMETRY_ANGLE;
        StarStreak.IsFTL = false;
    }
    protected override void Update()
    {
        Viewport.Geometry.MoveAtAngle(VIEWPORT_VELOCITY);
        base.Update();
    }
}
