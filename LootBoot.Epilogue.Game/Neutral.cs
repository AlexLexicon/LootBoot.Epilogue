namespace LootBoot.Epilogue.Game;
public class Neutral
{
    static Neutral() => Engine.Team.Map(new TeamInstance());
    public static IEnumerable<TTeamMembers> GetTargets<TTeamMembers>() where TTeamMembers : TeamMember => Team.GetTargets<TTeamMembers>();
    public static GameTeam Team => (GameTeam)Engine.Team.Get<TeamInstance>();
    private class TeamInstance : GameTeam
    {
        protected override List<GameTeam> GetGameTeamsWhenTargetingDefense()
        {
            return new List<GameTeam>
            {
                Enemy.Team,
                Player.Team,
            };
        }
        protected override List<GameTeam> GetGameTeamsWhenTargetingOffense() => null;
        public override Color Color => Color.DarkGray;
        public override bool IsIntegrityVisible => true;
        public override bool IsPositionTracked => true;
    }
}
