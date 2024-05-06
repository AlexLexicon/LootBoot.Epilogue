namespace LootBoot.Epilogue.Game.Scenes;
public class MenuScene : Scene
{
    public MenuScene() : base((int)Configurations.Scenes.Layers.Primary) { }
    protected override void Create()
    {
        Spacecraft = new Spacecraft(this, Player.Team);
        Spacecraft.Generate(Spacecrafts.MainMenu);
        base.Create();
    }
    public void StartExit() => Spacecraft.Controller.Complete();
    protected Spacecraft Spacecraft { get; set; }
}
