namespace LootBoot.Epilogue.Game.Models;
public abstract class GameTeam : Team
{
    public override IEnumerable<TTeamMember> GetTargets<TTeamMember>()
    {
        Dictionary<Type, Func<List<GameTeam>>> targetableTypesToPossibleGameTeams = new()
        {
            { typeof(Defense), GetGameTeamsWhenTargetingDefense },
            { typeof(Offense), GetGameTeamsWhenTargetingOffense },
        };
        foreach (var targetableTypeToPossibleGameTeams in targetableTypesToPossibleGameTeams)
        {
            if (Reflecting.IsOrDerivedFrom(typeof(TTeamMember), targetableTypeToPossibleGameTeams.Key))
            {
                List<GameTeam> gameTeams = targetableTypeToPossibleGameTeams.Value?.Invoke();
                if (gameTeams is not null)
                {
                    foreach (GameTeam gameTeam in gameTeams)
                    {
                        foreach (TTeamMember teamMember in gameTeam.GetMembers<TTeamMember>())
                        {
                            yield return teamMember;
                        }
                    }
                }
            }
        }
    }
    protected abstract List<GameTeam> GetGameTeamsWhenTargetingDefense();
    protected abstract List<GameTeam> GetGameTeamsWhenTargetingOffense();
    public abstract Color Color { get; }
    public abstract bool IsPositionTracked { get; }
    public abstract bool IsIntegrityVisible { get; }
}
