namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class BeetleFuselage : Fuselage
{
    public BeetleFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Beetle) => Sprites = new BeetleSprites(this);
}
