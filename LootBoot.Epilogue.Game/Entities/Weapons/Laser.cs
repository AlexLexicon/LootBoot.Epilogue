namespace LootBoot.Epilogue.Game.Entities.Weapons;
public sealed class Laser : Weapon
{
    public Laser(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats) : base(parent, team, baseGeometry, hardpoints, stats, Balance.Limits.Weapons.Laser) => Sprites = new LaserSprite(this);
}
