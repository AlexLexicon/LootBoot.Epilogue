namespace LootBoot.Epilogue.Game.Controllers.Spacecrafts;
public class MainMenuController : Controller
{
    public MainMenuController(Script parent) : base(parent) => Sequence<MainMenuEnterBehavior>().Complete().Start<MainMenuExitBehavior>();
    protected override void Create()
    {
        Start<MainMenuEnterBehavior>();
        base.Create();
    }
}
