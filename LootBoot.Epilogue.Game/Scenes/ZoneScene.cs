namespace LootBoot.Epilogue.Game.Scenes;
public class ZoneScene : Scene
{
    private const double VIEWPORT_VELOCITY = 500;
    private const double ASTROID_COUNT_MAX = 10;
    private const double SPAWN_NEXT_ASTROID_SECONDS_MAX = 5;
    private const double SPAWN_NEXT_ASTROID_SECONDS_MIN = 1;
    public event Action Completed;
    public event Action Transitioned;
    private DateTime? spawnNextAstroidTime;
    private double spawnNextAstroidSeconds;
    public ZoneScene() : base((int)Configurations.Scenes.Layers.Primary) { }
    protected override void Create()
    {
        LootBootEpilogue.IsPausable = true;
        Player.Zones.Current.Index += 1;
        Astroid.ResetScrapSpawned();
        Player.Actor.Create(this);
        Player.Actor.Spacecraft.Controller.Start<ZoneEnterStartBehavior>();
        Player.Actor.Spacecraft.Controller.When<ZoneEnterStopBehavior>().IsDestroyed().Run(StartBattle);
        Battle = SceneManager.ShowBattleScene(Player.Zones.Current.Index);
        Battle.OnComplete += Complete;
        base.Create();
    }
    protected override void Update()
    {
        if (IsZoneCompleted)
        {
            Viewport.Geometry.MoveAtAngle(VIEWPORT_VELOCITY);
        }
        else if (IsBattleStarted && Astroid.LivingAstroidsCount < ASTROID_COUNT_MAX)
        {
            if (Calculations.DateTime.IsSeconds(ref spawnNextAstroidTime, spawnNextAstroidSeconds))
            {
                new Astroid(this);
                spawnNextAstroidSeconds = Rng.ToDouble(SPAWN_NEXT_ASTROID_SECONDS_MIN, SPAWN_NEXT_ASTROID_SECONDS_MAX);
            }
        }
        base.Update();
    }
    protected void Complete()
    {
        IsBattleStarted = false;
        Player.Actor.Spacecraft.Controller.Stop();
        Player.Actor.Spacecraft.Controller.Start<ZoneExitRotateBehavior>();
        Player.Actor.Spacecraft.Controller.When<ZoneExitAccelerateBehavior>().IsCompleted().Run(CompleteZone);
        Completed?.Invoke();
    }
    protected void StartBattle()
    {
        Battle.Start();
        IsBattleStarted = true;
    }
    protected void CompleteZone()
    {
        Viewport.Watched.Set(null);
        Player.Actor.Spacecraft.QueueDestroy();
        Battle.QueueDestroy();
        IsZoneCompleted = true;
        Transitioned?.Invoke();
    }
    private Battle Battle { get; set; }
    public bool IsBattleStarted { get; protected set; }
    public bool IsZoneCompleted { get; protected set; }
}
