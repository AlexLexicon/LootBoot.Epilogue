namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class AnglerFuselage : Fuselage
{
    public AnglerFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Angler) => Sprites = new AnglerSprites(this);
}
