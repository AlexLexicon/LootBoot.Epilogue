namespace LootBoot.Epilogue.Game.Scenes;
public static class SceneManager
{
    public delegate void ShowDelegate(Scene scene);
    public static event ShowDelegate ShowZone;
    public static event ShowDelegate ShowMainMenu;
    public static event ShowDelegate ShowBattle;
    public static event ShowDelegate ShowTally;
    private static Type MainMenuType { get; } = typeof(MenuScene);
    private static Type ZoneType { get; } = typeof(ZoneScene);
    private static Type BattleType { get; } = typeof(Battle);
    private static Type TallyType { get; } = typeof(SummaryScene);
    public static TScene Show<TScene>() where TScene : Scene, new()
    {
        TScene scene = Create<TScene>();
        Type sceneType = typeof(TScene);
        if (sceneType == MainMenuType)
        {
            ShowMainMenu?.Invoke(scene);
        }
        else if (sceneType == ZoneType)
        {
            ShowZone?.Invoke(scene);
        }
        else if (sceneType == TallyType)
        {
            ShowTally?.Invoke(scene);
        }
        else if (Reflecting.IsOrDerivedFrom(sceneType, BattleType))
        {
            ShowBattle?.Invoke(scene);
        }
        return scene;
    }
    public static Battle ShowBattleScene(uint zone) => zone switch
    {
        1 => Show<Battle1>(),
        2 => Show<Battle2>(),
        3 => Show<Battle3>(),
        _ => null,
    };
    private static TScene Create<TScene>() where TScene : Scene, new() => Activator.CreateInstance<TScene>();
}
