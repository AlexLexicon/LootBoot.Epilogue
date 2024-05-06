namespace LootBoot.Epilogue.Engine;
public abstract class Team
{
    public static Team Get<TTeam>() where TTeam : Team, new()
    {
        Type teamType = typeof(TTeam);
        return TeamTypeToTeam.ContainsKey(teamType) ? TeamTypeToTeam[teamType] : null;
    }
    public static void Map<TTeam>(TTeam team) where TTeam : Team, new()
    {
        Type teamType = typeof(TTeam);
        if (TeamTypeToTeam.ContainsKey(teamType))
        {
            TeamTypeToTeam[teamType] = team;
        }
        else
        {
            TeamTypeToTeam.Add(teamType, team);
        }
    }
    private static Dictionary<Type, Team> TeamTypeToTeam { get; } = new Dictionary<Type, Team>();
    public Team()
    {
        TeamMembers = new();
        CachedTeamMembers = new();
    }
    public abstract IEnumerable<TTeamMember> GetTargets<TTeamMember>() where TTeamMember : TeamMember;
    protected IEnumerable<TTeamMember> GetMembers<TTeamMember>() where TTeamMember : TeamMember
    {
        Type teamMemberType = typeof(TTeamMember);
        IEnumerable<TTeamMember> teamMembers = null;
        if (CachedTeamMembers.ContainsKey(teamMemberType))
        {
            teamMembers = (IEnumerable<TTeamMember>)CachedTeamMembers[teamMemberType];
        }
        else
        {
            teamMembers = TeamMembers.Where(teamMember => Reflecting.IsOrDerivedFrom(teamMember?.GetType(), teamMemberType)).Cast<TTeamMember>();
            CachedTeamMembers.Add(teamMemberType, teamMembers);
        }
        if (teamMembers is not null)
        {
            foreach (TTeamMember teamMember in teamMembers)
            {
                yield return teamMember;
            }
        }
    }
    public void AddMember(TeamMember teamMember)
    {
        if (teamMember is not null && !TeamMembers.Contains(teamMember))
        {
            CachedTeamMembers.Clear();
            TeamMembers.Add(teamMember);
        }
    }
    public void RemoveMember(TeamMember teamMember)
    {
        if (teamMember is not null && TeamMembers.Contains(teamMember))
        {
            CachedTeamMembers.Clear();
            TeamMembers.Remove(teamMember);
        }
    }
    private HashSet<TeamMember> TeamMembers { get; }
    private Dictionary<Type, IEnumerable> CachedTeamMembers { get; }
    //public HashSet<TTeamMember> GetMembers<TTeamMember>() where TTeamMember : TeamMember
    //{
    //    Type teamMemberType = typeof(TTeamMember);
    //    if (TypeToTeamMember.ContainsKey(teamMemberType))
    //    {
    //        IEnumerable teamMembers = TypeToTeamMember[teamMemberType];
    //        return teamMembers.Cast<TTeamMember>().ToHashSet();
    //    }
    //    return null;
    //}
    //public void AddMember(TeamMember teamMember)
    //{
    //    if (teamMember is not null)
    //    {
    //        Type teamMemberType = teamMember.GetType();
    //        if (TypeToTeamMember.ContainsKey(teamMemberType))
    //        {
    //            IEnumerable teamMembers = TypeToTeamMember[teamMemberType];
    //            HashSet<TeamMember> set = (HashSet<TeamMember>)teamMembers;
    //            set.Add(teamMember);
    //            TypeToTeamMember[teamMemberType] = set;
    //        }
    //        else
    //        {
    //            HashSet<TeamMember> set = new()
    //            {
    //                teamMember,
    //            };
    //            TypeToTeamMember.Add(teamMemberType, set);
    //        }
    //    }
    //}
    //public void RemoveMember(TeamMember teamMember)
    //{
    //    if (teamMember is not null)
    //    {
    //        Type teamMemberType = teamMember.GetType();
    //        if (TypeToTeamMember.ContainsKey(teamMemberType))
    //        {
    //            IEnumerable teamMembers = TypeToTeamMember[teamMemberType];
    //            HashSet<TeamMember> set = (HashSet<TeamMember>)teamMembers;
    //            if (set.Contains(teamMember))
    //            {
    //                set.Remove(teamMember);
    //                TypeToTeamMember[teamMemberType] = set;
    //            }
    //        }
    //    }
    //}
    ////private HashSet<TeamMember> TeamMembers { get; }
    //private Dictionary<Type, IEnumerable> TypeToTeamMember { get; }
    //public HashSet<TTeamMember> GetMembers<TTeamMember>() where TTeamMember : TeamMember
    //{
    //    Type type = typeof(TTeamMember);
    //    if (CachedTypeToTeamMembers.ContainsKey(type))
    //    {
    //        IEnumerable set = CachedTypeToTeamMembers[type];
    //        return (HashSet<TTeamMember>)set;
    //    }
    //    else
    //    {
    //        HashSet<TTeamMember> teamMembers = TypeToTeamMember.Where(kvp => Reflecting.IsOrDerivedFrom(kvp.Key, type)).Select(kvp => kvp.Value).Cast<TTeamMember>().ToHashSet();
    //        CachedTypeToTeamMembers.Add(type, teamMembers);
    //        return teamMembers;
    //    }
    //}
    //public void AddMember<TTeamMember>(TTeamMember teamMember) where TTeamMember : TeamMember
    //{
    //    Type type = teamMember.GetType();
    //    if (!TypeToTeamMember.Select(kvp => kvp.Key == type).Any())
    //    {
    //        TypeToTeamMember.Add(new KeyValuePair<Type, TeamMember>(type, teamMember));
    //        CachedTypeToTeamMembers.Clear();
    //    }
    //}
    //public void RemoveMember<TTeamMember>(TTeamMember teamMember) where TTeamMember : TeamMember
    //{
    //    KeyValuePair<Type, TeamMember> kvpToRemove = TypeToTeamMember.Where(kvp => kvp.Value == teamMember).FirstOrDefault();
    //    if (!kvpToRemove.Equals(default(KeyValuePair<Type, TeamMember>)))
    //    {
    //        TypeToTeamMember.Remove(kvpToRemove);
    //        CachedTypeToTeamMembers.Clear();
    //    }
    //}
    //protected HashSet<KeyValuePair<Type, TeamMember>> TypeToTeamMember { get; }
    //private Dictionary<Type, IEnumerable> CachedTypeToTeamMembers { get; }
}
