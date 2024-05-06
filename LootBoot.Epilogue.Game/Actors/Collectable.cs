namespace LootBoot.Epilogue.Game.Actors;
public sealed class Collectable : Actor
{
    public Collectable(Scene scene) : base(scene, null) { }
    public Collectable(Scene scene, Definition definition) : base(scene, null) => Generate(definition);
    public Collectable(Scene scene, Definition definition, Point? spawnPosition, Angle? spawnAngle) : base(scene, null) => Generate(definition, spawnPosition, spawnAngle);
    public void Generate(Definition definition) => Generate(definition, null, null);
    public void Generate(Definition definition, Point? spawnPosition, Angle? spawnAngle)
    {
        Generate(definition.ItemStats, spawnPosition, spawnAngle);
        Summary = definition.Summary;
        Record = definition.Record;
    }
    public void Generate(ItemStats item, Point? spawnPosition, Angle? spawnAngle) => Item = item.Make switch
    {
        Makes.Items.Scrap => new Scrap(this, item, spawnPosition, spawnAngle),
        Makes.Items.Module => new Module(this, item, spawnPosition, spawnAngle),
        _ => null,
    };
    public void Collect()
    {
        if (!IsCollected)
        {
            IsCollected = true;
            Item.QueueDestroy();
            Player.Zones.Current.Summaries.Add(Summary);
            Player.Collection.Add(Record);
            QueueDestroy();
        }
    }
    public bool IsOccluded => Item is not null && Item.IsOccluded;
    public Item Item { get; private set; }
    public SummaryDefinition Summary { get; private set; }
    public RecordDefinition Record { get; private set; }
    public bool IsCollected { get; private set; }
    public struct Definition
    {
        public ItemStats ItemStats { get; init; }
        public SummaryDefinition Summary { get; init; }
        public RecordDefinition Record { get; init; }
    }
    public struct RecordDefinition
    {
        public string Name { get; init; }
        public string ImageFilePath { get; init; }
    }
}
