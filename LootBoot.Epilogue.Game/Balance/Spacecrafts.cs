namespace LootBoot.Epilogue.Game.Balance;
public static class Spacecrafts
{
    public static Spacecraft.Definition MainMenu => new()
    {
        GetController = (parent) => new MainMenuController(parent),
        CanDropCollectables = false,
        FuselageStats = new()
        {
            Make = Makes.Fuselages.Criterion,
        },
        WeaponStats = new()
        {
            Make = Makes.Weapons.Cannon,
            Count = 2,
        },
    };
    public static Spacecraft.Definition Angler => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Angler),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Angler,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Cannon,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Beetle => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Beetle),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Beetle,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Turret,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Criterion => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Criterion),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Criterion,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Cannon,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Eel => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Eel),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Eel,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Minelayer,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Firefly => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Firefly),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Firefly,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Flamer,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Gnat => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Gnat),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Gnat,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Cannon,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Mosquito => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Mosquito),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Mosquito,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Laser,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Parasite => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Parasite),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Parasite,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Cannon,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Spider => new()
    {
        GetController = (parent) => null,
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Spider),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Fuselages.Spider,
            Tech = Player.Zones.Current.Index,
            Velocity = new()
            {

            },
            Rotation = new()
            {

            },
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Arc,
            Tech = Player.Zones.Current.Index,
        },
    };
    public static Spacecraft.Definition Wasp => new()
    {
        GetController = (parent) => new WaspController(parent),
        CanDropCollectables = true,
        Record = new()
        {
            Name = nameof(Wasp),
            Category = SummaryDefinition.Categories.Spacecrafts,
            ImageFilePath = "sprites.fuselages.wasp.bulk.15-21.00-00-00.png",
            Verb = SummaryDefinition.Verbs.Destroyed,
        },
        FuselageStats = new()
        {

            Traits = new()
            {

            },
            Make = Makes.Fuselages.Wasp,
            Tech = Player.Zones.Current.Index,
            Integrity = 3,
            Velocity = new()
            {
                Maximum = 5,
                Minimum = 5,
                Acceleration = 1,
                Momentum = 1,
            },
            Rotation = new()
            {
                MinMax = 1,
                Acceleration = 1,
                Momentum = 1,
            }
        },
        WeaponStats = new()
        {
            Traits = new()
            {

            },
            Make = Makes.Weapons.Cannon,
            Tech = Player.Zones.Current.Index,
            Count = 2,
            Damage = 2,
            Accuracy = 3,
            FireRate = 5,
            Velocity = 4,
        },
    };
}
