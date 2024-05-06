namespace LootBoot.Epilogue.Game.Controllers;
public class BattleController : Controller
{
    public BattleController(Script parent) : base(parent)
    {
    }
    protected override void Create()
    {
        Start<BattleBehavior>();
        base.Create();
    }
}
