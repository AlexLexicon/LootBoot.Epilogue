﻿namespace LootBoot.Epilogue.Game.Sprites.Fuselages;
public class SpiderSprites : FuselageSprites
{
    public SpiderSprites(Fuselage parent) : base(parent) { }
    protected override ObservableCollection<string> BulkImageFilePaths => new()
    {

    };
    protected override ObservableCollection<string> EngineImageFilePaths => new()
    {

    };
    protected override HardpointCollection NewHardpoints() => new()
    {

    };
    protected override void AnimationChange() => throw new NotImplementedException();
}
