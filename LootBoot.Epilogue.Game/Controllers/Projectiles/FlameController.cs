namespace LootBoot.Epilogue.Game.Controllers.Projectiles;
public class FlameController : Controller
{
    public FlameController(Script parent) : base(parent) => Sequence<FlameBehavior>().Complete().Start<DestroyBehavior>();
    protected override void Create()
    {
        Start<FlameBehavior>();
        base.Create();
    }
}
