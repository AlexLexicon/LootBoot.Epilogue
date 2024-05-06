namespace LootBoot.Epilogue.Game.Sprites.Fuselages;
public class WaspSprites : FuselageSprites
{
    public WaspSprites(Fuselage parent) : base(parent) { }
    protected override ObservableCollection<string> BulkImageFilePaths => new()
    {
        "sprites.fuselages.wasp.bulk.15-21.00-00-00.png"
    };
    protected override ObservableCollection<string> EngineImageFilePaths => new()
    {
        "sprites.fuselages.wasp.engine.15-21.00-00-00.png",
        "sprites.fuselages.wasp.engine.15-21.00-01-00.png",
        "sprites.fuselages.wasp.engine.15-21.01-00-00.png",
        "sprites.fuselages.wasp.engine.15-21.01-01-00.png",
        "sprites.fuselages.wasp.engine.15-21.02-00-00.png",
        "sprites.fuselages.wasp.engine.15-21.02-01-00.png",
    };
    protected override HardpointCollection NewHardpoints() => new()
    {
        new Hardpoint[]
        {
            new Hardpoint(0, 0, 0, -0.1),
        },
        new Hardpoint[]
        {
            new Hardpoint(-0.25, -0.12, -0.25, -0.1),
            new Hardpoint(0.25, -0.12, 0.25, -0.1),
        }
    };
    protected override void AnimationChange()
    {
        switch (EngineAnimation)
        {
            default:
            case EngineAnimations.Idle:
                Engine.AnimationSeconds = 0.6;
                Engine.AnimationIndex = 0;
                Engine.AnimationIndexMinimum = 0;
                Engine.AnimationIndexMaximum = 1;
                Engine.AnimationIndexDirection = SpriteAnimation.Directions.Ascending;
                break;
            case EngineAnimations.Thrust:
                Engine.AnimationSeconds = 0.3;
                Engine.AnimationIndex = 2;
                Engine.AnimationIndexMinimum = 2;
                Engine.AnimationIndexMaximum = 3;
                Engine.AnimationIndexDirection = SpriteAnimation.Directions.Ascending;
                break;
            case EngineAnimations.Reverse:
                Engine.AnimationSeconds = 0.4;
                Engine.AnimationIndex = 4;
                Engine.AnimationIndexMinimum = 4;
                Engine.AnimationIndexMaximum = 5;
                Engine.AnimationIndexDirection = SpriteAnimation.Directions.Descending;
                break;
        }
    }
}
