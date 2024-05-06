namespace LootBoot.Epilogue.Game.TeamMembers;
public abstract class Combat : TeamMember
{
    public Combat(Script parent, GameTeam team, Geometry collisionGeometry) : base(parent, team) => CollisionGeometry = collisionGeometry;
    public new GameTeam Team => (GameTeam)base.Team;
    public Geometry CollisionGeometry { get; }
}
