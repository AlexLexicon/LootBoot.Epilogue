namespace LootBoot.Epilogue.Game.Entities.Fuselages;
public sealed class EelFuselage : Fuselage
{
    public EelFuselage(Spacecraft parent, GameTeam team, FuselageStats stats) : base(parent, team, stats, Balance.Limits.Fuselages.Eel) => Sprites = new EelSprites(this);
}
