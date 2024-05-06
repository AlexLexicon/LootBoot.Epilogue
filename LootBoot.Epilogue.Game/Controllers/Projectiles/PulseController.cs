namespace LootBoot.Epilogue.Game.Controllers.Projectiles;
public class PulseController : Controller
{
    public PulseController(Script parent) : base(parent) => Sequence<PulseBehavior>().Complete().Start<DestroyBehavior>();
    protected override void Create()
    {
        Start<PulseBehavior>();
        base.Create();
    }
}
