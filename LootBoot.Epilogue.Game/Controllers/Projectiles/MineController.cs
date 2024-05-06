namespace LootBoot.Epilogue.Game.Controllers.Projectiles;
public class MineController : Controller
{
    public MineController(Script parent) : base(parent) => Sequence<MineBehavior>().Complete().Start<DestroyBehavior>();
    protected override void Create()
    {
        Start<MineBehavior>();
        base.Create();
    }
}
