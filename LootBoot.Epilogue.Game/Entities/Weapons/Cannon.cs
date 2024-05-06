namespace LootBoot.Epilogue.Game.Entities.Weapons;
public sealed class Cannon : Weapon
{
    public Cannon(Spacecraft parent, GameTeam team, Geometry baseGeometry, Hardpoint[] hardpoints, WeaponStats stats) : base(parent, team, baseGeometry, hardpoints, stats, Balance.Limits.Weapons.Cannon)
    {
        Sprites = new CannonSprite(this);
        Controller = new Controller(this);
    }
    protected override void Create()
    {
        Controller.Start<CannonBehavior>();
        base.Create();
    }
}
