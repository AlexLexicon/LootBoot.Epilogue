namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class CriterionFuselage : Fuselage
{
    public CriterionFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Criterion) => Sprites = new CriterionSprites(this);
}
