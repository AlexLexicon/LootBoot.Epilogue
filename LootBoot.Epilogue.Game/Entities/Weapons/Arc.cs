namespace LootBoot.Epilogue.Game.Entities.Weapons;
public sealed class Arc : Weapon
{
    public Arc(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats) : base(parent, team, baseGeometry, hardpoints, stats, Balance.Limits.Weapons.Arc) => Sprites = new ArcSprite(this);
}
