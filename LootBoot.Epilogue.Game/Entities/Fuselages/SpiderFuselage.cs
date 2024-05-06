namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class SpiderFuselage : Fuselage
{
    public SpiderFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Spider) => Sprites = new SpiderSprites(this);
}
