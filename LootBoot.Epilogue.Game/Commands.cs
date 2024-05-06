using LootBoot.Epilogue.Game.Actors;

namespace LootBoot.Epilogue.Game;
public sealed class Commands
{
    private const string COMMAND_DELIMTER = ".";
    private const string WEAPON = "Weapon";
    private const string FUSELAGE = "Fuselage";
    private const string STATS = "Stats";
    private const string MAKE = "Make";
    private const string COUNT = "Count";
    private const string TECH = "Tech";
    private const string DAMAGE = "Damage";
    private const string ACCURACY = "Accuracy";
    private const string VELOCITY = "Velocity";
    private const string FIRERATE = "FireRate";
    private const string INTEGRITY = "Integrity";
    private const string ROTATION = "Rotation";
    private const string GEOMETRY = "Geometry";
    private const string POSITION = "Position";
    private const string ANGLE = "Angle";
    private const string SIZE = "Size";
    private const string X = "x";
    private const string Y = "y";
    private const string WIDTH = "Width";
    private const string HEIGHT = "Height";
    private const string MAXIMUM = "Maximum";
    private const string MINIMUM = "Minimum";
    private const string ACCELERATION = "Acceleration";
    private const string MOMENTUM = "Momentum";
    private const string MINMAX = "MinMax";
    public Commands()
    {
        GameConsole.OnCommand += Parse;
    }
    public void CompileActors()
    {
        List<Actor> actors = ActorInterrogator.Interrogate();
        ActorWithIdentifiers = new Dictionary<ActorIdentifier, Actor>();
        int id = 100;
        foreach (Actor actor in actors)
        {
            if (actor is not null)
            {
                string name = actor.GetType().Name;
                if (actor is Spacecraft spacecraft)
                {
                    name += $" [{spacecraft.Fuselage?.GetType().Name}/{spacecraft.Weapon?.GetType().Name}]";
                }
                ActorIdentifier actorIdentifier = new ActorIdentifier(id.ToString(), name);
                if (actor == Player.Actor.Spacecraft)
                {
                    PlayerAlias = new KeyValuePair<string, ActorIdentifier>("player", actorIdentifier);
                }
                ActorWithIdentifiers.Add(actorIdentifier, actor);
                id++;
            }
        }
    }
    private void Parse(string message)
    {
        if (message is not null && message.Contains(COMMAND_DELIMTER))
        {
            string evaluation = null;
            if (message.Contains('='))
            {
                var p = message.Split('=');
                message = p[0].Trim();
                evaluation = p[1].Trim();
            }
            string[] sections = message.Split(COMMAND_DELIMTER);
            if (sections is not null && sections.Length > 1)
            {
                (ActorIdentifier? aid, Actor actor) = GetActor(sections[0]);
                if (aid is not null && actor is not null)
                {
                    if (actor is Spacecraft spacecraft)
                    {
                        List<string> options = new List<string>
                        {
                            WEAPON,
                            FUSELAGE,
                        };
                        CommandSelection cs = new CommandSelection(null, null);
                        for (int index = 1; index < sections.Length; index++)
                        {
                            string section = sections[index]?.Trim();

                            bool isLastSection = index == sections.Length - 1;
                            //if (isLastSection && section.Contains('='))
                            //{
                            //    string[] parts = section.Split('=');
                            //    section = parts[0].Trim();
                            //    evaluation = string.Empty;
                            //    for (int pIndex = 1; pIndex < parts.Length; pIndex++)
                            //    {
                            //        evaluation += parts[pIndex].Trim();
                            //    }
                            //}
                            (CommandSelection? nextCs, List<string> nextOptions) = GetOptionsFromSection(new CommandSelection(section, cs), options);
                            if (nextCs is null)
                            {
                                break;
                            }
                            cs = nextCs.Value;
                            if (isLastSection)
                            {
                                if (cs.CurrentSelection == MAKE)
                                {
                                    if (cs.AllSelections.Contains(WEAPON))
                                    {
                                        if (evaluation is null)
                                        {
                                            Execute_GetWeaponMake(aid.Value, spacecraft);
                                        }
                                        else
                                        {
                                            Execute_SetWeaponMake(aid.Value, spacecraft, evaluation);
                                        }
                                        return;
                                    }
                                    else if (cs.AllSelections.Contains(FUSELAGE))
                                    {
                                        if (evaluation is null)
                                        {
                                            Execute_GetFuselageMake(aid.Value, spacecraft);
                                        }
                                        else
                                        {
                                            Execute_SetFuselageMake(aid.Value, spacecraft, evaluation);
                                        }
                                        return;
                                    }
                                    CouldNotParse();
                                    return;
                                }
                            }
                            options = nextOptions;
                        }
                    }
                }
            }
        }
    }
    private void WriteCommand(string value)
    {
        GameConsole.Write(value);
    }
    private void CouldNotParse()
    {

    }
    private void Execute_GetWeaponMake(ActorIdentifier aid, Spacecraft spacecraft)
    {
        WriteCommand($"{aid.Id}.Weapon.Attributes.Stats.Make = '{spacecraft.Weapon.Attributes.Stats.Make}'");
    }
    private void Execute_SetWeaponMake(ActorIdentifier aid, Spacecraft spacecraft, string evaluate)
    {
        if (evaluate is not null)
        {
            foreach (Makes make in Enum.GetValues(typeof(Makes.Weapons)))
            {
                if (evaluate.ToLower() == make.ToString().ToLower())
                {

                }
            }
        }
    }
    private void Execute_GetFuselageMake(ActorIdentifier aid, Spacecraft spacecraft)
    {
        WriteCommand($"{aid.Id}.Fuselage.Attributes.Stats.Make = '{spacecraft.Fuselage.Attributes.Stats.Make}'");
    }
    private void Execute_SetFuselageMake(ActorIdentifier aid, Spacecraft spacecraft, string evaluate)
    {
        if (evaluate is not null)
        {
            foreach (Makes.Fuselages make in Enum.GetValues(typeof(Makes.Fuselages)))
            {
                if (evaluate.ToLower() == make.ToString().ToLower())
                {
                    var current = spacecraft.Fuselage.Attributes.Stats;
                    FuselageStats stats = new FuselageStats
                    {
                        Make = make,
                        Integrity = current.Integrity,
                        Tech = current.Tech,
                        Traits = current.Traits,
                        Velocity = current.Velocity,
                        Rotation = current.Rotation,
                    };
                    //spacecraft.Generate();
                    WriteCommand($"{aid.Id}.Fuselage.Attributes.Stats.Make => '{spacecraft.Fuselage.Attributes.Stats.Make}'");
                    return;
                }
            }
        }
    }
    private (CommandSelection? cs, List<string> options) GetOptionsFromSection(CommandSelection cs, List<string> options)
    {
        if (!string.IsNullOrWhiteSpace(cs.CurrentSelection) && options is not null && options.Count > 0)
        {
            foreach (string option in options)
            {
                if (!string.IsNullOrWhiteSpace(option))
                {
                    if (cs.CurrentSelection.ToLower() == option.ToLower())
                    {
                        List<string> nextOptions = option switch
                        {
                            WEAPON => new List<string>
                        {
                            STATS,
                        },
                            FUSELAGE => new List<string>
                        {
                            STATS,
                            GEOMETRY,
                        },
                            STATS => new List<string>
                        {
                            MAKE,
                            COUNT,
                            TECH,
                            DAMAGE,
                            ACCURACY,
                            VELOCITY,
                            FIRERATE,
                            INTEGRITY,
                            ROTATION,
                        },
                            VELOCITY => new List<string>
                        {
                            MAXIMUM,
                            MINIMUM,
                            ACCELERATION,
                            MOMENTUM
                        },
                            ROTATION => new List<string>
                        {
                            MINMAX,
                            ACCELERATION,
                            MOMENTUM,
                        },
                            GEOMETRY => new List<string>
                        {
                            POSITION,
                            ANGLE,
                            SIZE,
                        },
                            POSITION => new List<string>
                        {
                            X,
                            Y,
                        },
                            SIZE => new List<string>
                        {
                            WIDTH,
                            HEIGHT,
                        },
                            _ => null,
                        };
                        return (new CommandSelection(option, cs), nextOptions);
                    }
                }
            }
        }
        return (null, null);
    }
    private (ActorIdentifier? aid, Actor actor) GetActor(string identifier)
    {
        //string actorIdentifierOrAlias = identifier[..identifier.IndexOf(COMMAND_DELIMTER)];
        if (!string.IsNullOrWhiteSpace(identifier))
        {
            identifier = identifier.ToLower();
            ActorIdentifier id;
            if (identifier == PlayerAlias.Key)
            {
                id = PlayerAlias.Value;
            }
            else
            {
                id = new ActorIdentifier(identifier);
            }
            if (ActorWithIdentifiers.ContainsKey(id))
            {
                return (id, ActorWithIdentifiers[id]);
            }
        }
        return (null, null);
    }
    public Dictionary<ActorIdentifier, Actor> ActorWithIdentifiers { get; private set; }
    public KeyValuePair<string, ActorIdentifier> PlayerAlias { get; private set; }
    public struct ActorIdentifier
    {
        public ActorIdentifier(string id) : this(id, null) { }
        public ActorIdentifier(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is ActorIdentifier ai)
            {
                return ai.Id == Id;
            }
            return false;
        }
        public override int GetHashCode() => Id.GetHashCode();
    }
    private struct CommandSelection
    {
        public CommandSelection(string currentSelection, CommandSelection commandSelection)
        {
            CurrentSelection = currentSelection;
            AllSelections = new List<string>();
            if (commandSelection.AllSelections is not null)
            {
                AllSelections.AddRange(commandSelection.AllSelections);
            }
            if (CurrentSelection is not null)
            {
                AllSelections.Add(CurrentSelection);
            }
        }
        public CommandSelection(string currentSelection, List<string> allSelections)
        {
            CurrentSelection = currentSelection;
            AllSelections = allSelections;
        }
        public string CurrentSelection { get; }
        public List<string> AllSelections { get; }
    }
}
