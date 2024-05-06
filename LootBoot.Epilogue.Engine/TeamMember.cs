namespace LootBoot.Epilogue.Engine;
public abstract class TeamMember : Script
{
    public TeamMember(Script parent, Team team) : base(parent) => Team = team;
    protected override void Create()
    {
        Team.AddMember(this);
        base.Create();
    }
    protected override void Destroy()
    {
        Team.RemoveMember(this);
        base.Destroy();
    }
    public Team Team { get; }
}
