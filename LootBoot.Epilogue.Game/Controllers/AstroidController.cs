namespace LootBoot.Epilogue.Game.Controllers;
public class AstroidController : Controller
{
    public AstroidController(Astroid astroid) : base(astroid)
    {
        When<AstroidBehavior>().IsCompleted().Run(() => astroid.QueueDestroy());
    }
}
