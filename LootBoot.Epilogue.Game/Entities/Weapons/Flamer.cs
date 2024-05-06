namespace LootBoot.Epilogue.Game.Entities.Weapons;
public sealed class Flamer : Weapon
{
    public Flamer(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats) : base(parent, team, baseGeometry, hardpoints, stats, Balance.Limits.Weapons.Flamer) => Sprites = new FlamerSprite(this);
}
