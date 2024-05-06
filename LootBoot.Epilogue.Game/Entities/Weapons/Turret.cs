namespace LootBoot.Epilogue.Game.Entities.Weapons;
public sealed class Turret : Weapon
{
    public Turret(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats) : base(parent, team, baseGeometry, hardpoints, stats, Balance.Limits.Weapons.Turret) => Sprites = new TurretSprite(this);
}
