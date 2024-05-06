namespace LootBoot.Epilogue.Game.TeamMembers;
public class Defense : Combat
{
    public event Action OnHit;
    public event Action OnKill;
    public event Action OnMiss;
    private readonly HashSet<Offense> _missed;
    public Defense(Script parent, GameTeam team, Geometry collisionGeometry) : base(parent, team, collisionGeometry)
    {
        _missed = new HashSet<Offense>();
        IsMissable = true;
        IsAbleToBeHit = true;
    }
    public virtual void Hit(Offense offense)
    {
        if (!IsKilled)
        {
            Hits++;
            LatestAttacker = offense;
            OnHit?.Invoke();
        }
    }
    public virtual void Kill()
    {
        IsKilled = true;
        OnKill?.Invoke();
    }
    public void Miss(Offense offense)
    {
        if (!_missed.Contains(offense))
        {
            _missed.Add(offense);
            OnMiss?.Invoke();
        }
    }
    public Offense LatestAttacker { get; protected set; }
    public int Hits { get; protected set; }
    public bool IsKilled { get; protected set; }
    public bool IsAbleToBeHit { get; set; }
    public bool IsMissable { get; set; }
}
