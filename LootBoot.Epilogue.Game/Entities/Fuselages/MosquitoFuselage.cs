namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class MosquitoFuselage : Fuselage
{
    public MosquitoFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Mosquito) => Sprites = new MosquitoSprites(this);
}
