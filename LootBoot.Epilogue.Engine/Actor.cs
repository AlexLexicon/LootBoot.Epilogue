namespace LootBoot.Epilogue.Engine;
public class Actor : Script
{
    public Actor(Scene parent, Team team) : base(parent) => Team = team;
    public new Scene Parent => (Scene)base.Parent;
    public Team Team { get; }
}
