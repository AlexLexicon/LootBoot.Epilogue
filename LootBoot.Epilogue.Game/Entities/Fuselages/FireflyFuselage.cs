namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class FireflyFuselage : Fuselage
{
    public FireflyFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Firefly) => Sprites = new FireflySprites(this);
}
