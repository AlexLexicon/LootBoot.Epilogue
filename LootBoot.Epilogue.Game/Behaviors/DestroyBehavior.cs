namespace LootBoot.Epilogue.Game.Behaviors;
public class DestroyBehavior : Behavior
{
    protected override void Create()
    {
        Source.QueueDestroy();
        Complete();
        base.Create();
    }
}
