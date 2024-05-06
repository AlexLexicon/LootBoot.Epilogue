namespace LootBoot.Epilogue.Game.Balance;
public static class Collectables
{
    public static class Records
    {
        public static HashSet<Collectable.RecordDefinition> All = new()
        {
            Scrap,
            RareModule,
            RemarkableModule,
        };
        public static Collectable.RecordDefinition Scrap => new()
        {
            ImageFilePath = "sprites.items.scrap.13.00-00-00.png",
            Name = "Scrap",
        };
        public static Collectable.RecordDefinition RareModule => new()
        {
            ImageFilePath = "sprites.items.module.13.00-00-00.png",
            Name = "Rare Module",
        };
        public static Collectable.RecordDefinition RemarkableModule => new()
        {
            ImageFilePath = "sprites.items.module.13.01-00-00.png",
            Name = "Remarkable Module",
        };
    }
    public static Collectable.Definition Scrap => new()
    {
        ItemStats = new()
        {
            Make = Makes.Items.Scrap,
            Velocity = new(),
        },
        Summary = new()
        {
            Category = SummaryDefinition.Categories.Items,
            ImageFilePath = Records.Scrap.ImageFilePath,
            Name = Records.Scrap.Name,
            Verb = SummaryDefinition.Verbs.Collected,
        },
        Record = Records.Scrap,
    };
    public static Collectable.Definition RareModule => new()
    {
        ItemStats = new()
        {
            Make = Makes.Items.Module,
            Rarity = Rarity.Rare,
            Velocity = new(),
        },
        Summary = new()
        {
            Category = SummaryDefinition.Categories.Items,
            ImageFilePath = Records.RareModule.ImageFilePath,
            Name = Records.RareModule.Name,
            Verb = SummaryDefinition.Verbs.Collected,
        },
        Record = Records.RareModule,
    };
    public static Collectable.Definition RemarkableModule => new()
    {
        ItemStats = new()
        {
            Make = Makes.Items.Module,
            Rarity = Rarity.Remarkable,
            Velocity = new(),
        },
        Summary = new()
        {
            Category = SummaryDefinition.Categories.Items,
            ImageFilePath = Records.RemarkableModule.ImageFilePath,
            Name = Records.RemarkableModule.Name,
            Verb = SummaryDefinition.Verbs.Collected,
        },
        Record = Records.RemarkableModule,
    };
}
