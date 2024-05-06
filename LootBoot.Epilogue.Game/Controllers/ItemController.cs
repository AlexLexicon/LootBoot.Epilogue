namespace LootBoot.Epilogue.Game.Controllers;
public class ItemController : Controller
{
    public event Action OnPickedUp;
    public ItemController(Script parent) : base(parent)
    {
        When<ItemBehavior>().IsCompleted().Run(() => OnPickedUp?.Invoke());
    }
    protected override void Create()
    {
        Start<ItemBehavior>();
        base.Create();
    }
}
