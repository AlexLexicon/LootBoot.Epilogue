namespace LootBoot.Epilogue.Game.Entities.Weapons;
public sealed class Minelayer : Weapon
{
    public Minelayer(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats) : base(parent, team, baseGeometry, hardpoints, stats, Balance.Limits.Weapons.Minelayer) => Sprites = new MinelayerSprite(this);
}
