namespace LootBoot.Epilogue.Game.Controllers.Projectiles;
public class BeamController : Controller
{
    public BeamController(Script parent) : base(parent) => Sequence<BeamBehavior>().Complete().Start<DestroyBehavior>();
    protected override void Create()
    {
        Start<BeamBehavior>();
        base.Create();
    }
}
