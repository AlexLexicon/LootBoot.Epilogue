namespace LootBoot.Epilogue.Game.Summaries;
public struct SummaryDefinition
{
    public enum Categories
    {
        Spacecrafts,
        Items,
    }
    public enum Verbs
    {
        Destroyed,
        Collected,
    }
    public string ImageFilePath { get; init; }
    public Categories Category { get; init; }
    public string Name { get; init; }
    public Verbs Verb { get; init; }
}
