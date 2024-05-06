namespace LootBoot.Epilogue.Game.Controllers.Spacecrafts;
public class PlayerController : Controller
{
    //public event Action ZoneEnterStopCompleted;
    //public event Action ZoneExitAccelerateCompleted;
    public PlayerController(Script parent) : base(parent)
    {
        Sequence<ZoneEnterStartBehavior>().Destroy().Start<ZoneEnterWaitBehavior>();
        Sequence<ZoneEnterWaitBehavior>().Destroy().Start<ZoneEnterStopBehavior>();
        Sequence<ZoneEnterStopBehavior>().Destroy().Start<HumanStrategyBehavior>();
        Sequence<ZoneExitRotateBehavior>().Destroy().Start<ZoneExitAccelerateBehavior>();
        //Sequence<ZoneExitAccelerateBehavior>().OnDestroy().Start<ZoneExitWaitBehavior>();
        //When<ZoneExitAccelerateBehavior>().IsCompleted().Run(() => ZoneExitAccelerateCompleted?.Invoke());
        //When<ZoneEnterStopBehavior>().IsCompleted().Run(() => ZoneEnterStopCompleted?.Invoke());
    }
    //protected override void BehaviorComplete(Behavior behavior)
    //{
    //    if (behavior is ZoneEnterStopBehavior)
    //    {
    //        ZoneEnterStopCompleted?.Invoke();
    //    }
    //}
}
