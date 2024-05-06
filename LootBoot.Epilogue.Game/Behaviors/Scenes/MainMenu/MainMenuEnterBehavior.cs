namespace LootBoot.Epilogue.Game.Behaviors.Scenes.MainMenu;
public class MainMenuEnterBehavior : MainMenuBehavior
{
    protected override void Create()
    {
        base.Create();
        Viewport.Watched.Set(Geometry);
        StarStreak.IsFTL = true;
    }
}
