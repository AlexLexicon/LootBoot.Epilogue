namespace LootBoot.Epilogue.Game.TeamMembers;
public class Offense : Combat
{
    public event Action OnAttack;
    public Offense(Script parent, GameTeam team, Geometry collisionGeometry) : base(parent, team, collisionGeometry) { }
    public virtual bool Attack()
    {
        bool isIntersecting = false;
        IEnumerable<Defense> defenses = Team.GetTargets<Defense>();
        if (defenses is not null)
        {
            foreach (Defense defense in defenses)
            {
                if (defense.IsAbleToBeHit)
                {
                    Rect defenseCollisionBox = defense.CollisionGeometry.Bounds.Screen;
                    Rect offenseCollisionBox = CollisionGeometry.Bounds.Screen;
                    if (defenseCollisionBox.IntersectsWith(offenseCollisionBox))
                    {
                        isIntersecting = true;
                        if (IsAbleToHit || !defense.IsMissable)
                        {
                            LatestDefender = defense;
                            defense.Hit(this);
                            OnAttack?.Invoke();
                            return true;
                        }
                        else
                        {
                            defense.Miss(this);
                        }
                    }
                }
            }
        }
        if (!isIntersecting)
        {
            IsAbleToHit = true;
        }
        return false;
    }
    public Defense LatestDefender { get; protected set; }
    public bool IsAbleToHit { get; protected set; }
}
