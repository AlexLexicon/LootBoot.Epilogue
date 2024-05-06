namespace LootBoot.Epilogue.ViewModels;
public class SceneViewModel<TScene> : ViewModel where TScene : Scene
{
    public SceneViewModel(TScene scene)
    {
        if (scene is null)
        {
            throw new Exception("The provided scene cannot be null when creating a SceneViewModel");
        }
        Scene = scene;
    }
    public TScene Scene { get; }
}
