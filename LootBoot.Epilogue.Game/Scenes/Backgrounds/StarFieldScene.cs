namespace LootBoot.Epilogue.Game.Scenes.Backgrounds;
public class StarFieldScene : Scene
{
    public const int TOTAL_STARPOINTS = 30;
    public const int TOTAL_STARSTREAKS = 10;
    public const int TOTAL_PLANETS = 0;
    public StarFieldScene() : base((int)Configurations.Scenes.Layers.Background) { }
    protected override void Create()
    {
        for (int count = 0; count < TOTAL_STARPOINTS; count++)
        {
            new StarPoint(this);
        }
        for (int count = 0; count < TOTAL_STARSTREAKS; count++)
        {
            new StarStreak(this);
        }
        for (int count = 0; count < TOTAL_PLANETS; count++)
        {
            new Planet(this);
        }
        base.Create();
    }
    protected override void Update() => base.Update();
}
