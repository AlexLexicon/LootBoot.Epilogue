namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class ParasiteFuselage : Fuselage
{
    public ParasiteFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Parasite) => Sprites = new ParasiteSprites(this);
}
