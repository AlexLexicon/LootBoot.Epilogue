namespace LootBoot.Epilogue.Game.Sprites.Fuselages;
public class CriterionSprites : FuselageSprites
{
    public CriterionSprites(Fuselage parent) : base(parent) { }
    protected override ObservableCollection<string> BulkImageFilePaths => new()
    {
        "sprites.fuselages.criterion.bulk.15-21.00-00-00.png"
    };
    protected override ObservableCollection<string> EngineImageFilePaths => new()
    {
        "sprites.fuselages.criterion.engine.15-21.00-00-00.png",
        "sprites.fuselages.criterion.engine.15-21.00-01-00.png",
        "sprites.fuselages.criterion.engine.15-21.01-00-00.png",
        "sprites.fuselages.criterion.engine.15-21.01-01-00.png",
        "sprites.fuselages.criterion.engine.15-21.02-00-00.png",
        "sprites.fuselages.criterion.engine.15-21.02-01-00.png",
    };
    protected override HardpointCollection NewHardpoints() => new()
    {
        new Hardpoint[]
        {
            new Hardpoint(0, -0.3, 0, -0.45),
        },
        new Hardpoint[]
        {
            new Hardpoint(-0.25, 0.1, -0.25, -0.1),
            new Hardpoint(0.25, 0.1, 0.25, -0.1),
        },
        new Hardpoint[]
        {
            new Hardpoint(0, -0.3, 0, -0.45),
            new Hardpoint(-0.25, 0.1, -0.25, -0.1),
            new Hardpoint(0.25, 0.1, 0.25, -0.1),
        },
        new Hardpoint[]
        {
            new Hardpoint(-0.20, 0.1, -0.20, -0.1),
            new Hardpoint(-0.30, 0.1, -0.30, -0.1),
            new Hardpoint(0.2, 0.1, 0.20, -0.1),
            new Hardpoint(0.30, 0.1, 0.30, -0.1),
        },
        new Hardpoint[]
        {
            new Hardpoint(-0.20, 0.1, -0.20, -0.1),
            new Hardpoint(-0.30, 0.1, -0.30, -0.1),
            new Hardpoint(0.20, 0.1, 0.20, -0.1),
            new Hardpoint(0.30, 0.1, 0.30, -0.1),
            new Hardpoint(0, -0.3, 0, -0.45),
        },
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
