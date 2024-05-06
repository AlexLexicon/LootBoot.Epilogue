namespace LootBoot.Epilogue.Game.Controllers.Spacecrafts;
public class WaspController : Controller
{
    public WaspController(Script parent) : base(parent)
    {
        Sequence<SpawnBehavior>().Complete().Start<ChaseBehavior>();
        Sequence<ChaseBehavior>().Complete().Start<RetreatBehavior>();
        Sequence<RetreatBehavior>().Complete().Start<MusterBehavior>();
        Sequence<MusterBehavior>().Complete().Start<ChaseBehavior>();
    }
    protected override void Create()
    {
        Start<SpawnBehavior>();
        base.Create();
    }
}
