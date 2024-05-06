namespace LootBoot.Epilogue.Game.Controllers.Projectiles;
public class SwellController : Controller
{
    public SwellController(Script parent) : base(parent) => Sequence<SwellBehavior>().Complete().Start<DestroyBehavior>();
    protected override void Create()
    {
        Start<SwellBehavior>();
        base.Create();
    }
}
