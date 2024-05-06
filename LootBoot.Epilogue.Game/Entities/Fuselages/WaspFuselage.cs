namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class WaspFuselage : Fuselage
{
    public WaspFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Wasp) => Sprites = new WaspSprites(this);
}
