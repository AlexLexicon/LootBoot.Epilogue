namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class GnatFuselage : Fuselage
{
    public GnatFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Gnat) => Sprites = new GnatSprites(this);
}
