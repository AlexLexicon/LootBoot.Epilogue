namespace LootBoot.Epilogue.Game.Entities;
public abstract class Projectile : Entity
{
    protected Projectile(Scene parent, GameTeam team) : base(parent)
    {
        Offense = new Offense(this, team, Geometry);
        Offense.OnAttack += Attack;
    }
    protected virtual void Attack() { }
    public new Scene Parent => (Scene)base.Parent;
    public Offense Offense { get; }
    public Sprite Sprite { get; protected set; }
}
