namespace LootBoot.Epilogue.Game;
public static class Player
{
    static Player() => Engine.Team.Map(new TeamInstance());
    public static IEnumerable<TTeamMembers> GetTargets<TTeamMembers>() where TTeamMembers : TeamMember => Team.GetTargets<TTeamMembers>();
    public static GameTeam Team => (GameTeam)Engine.Team.Get<TeamInstance>();
    private class TeamInstance : GameTeam
    {
        protected override List<GameTeam> GetGameTeamsWhenTargetingDefense()
        {
            return new List<GameTeam>
                {
                    Enemy.Team,
                    Neutral.Team,
                };
        }
        protected override List<GameTeam> GetGameTeamsWhenTargetingOffense() => null;
        public override Color Color => Color.Red;
        public override bool IsIntegrityVisible => false;
        public override bool IsPositionTracked => false;
    }
    public static class Geometry
    {
        public static Size? Size => Actor.Spacecraft?.Fuselage?.Geometry?.Size;
        public static Point? Position => Actor.Spacecraft?.Fuselage?.Geometry?.Position;
        public static double? DistanceTo(Engine.Geometry geometry) => DistanceTo(geometry.Position);
        public static double? DistanceTo(Point point) => Actor.Spacecraft?.Fuselage?.Geometry?.DistanceToPosition(point);
        public static Angle? AngleTo(Engine.Geometry geometry)
        {
            Point? target = Position;
            if (target is null)
            {
                return null;
            }
            return geometry.AngleToTarget(target.Value);
        }
    }
    public static class Actor
    {
        public static event Action Hit;
        public static void Create(Scene scene)
        {
            Spacecraft = new(scene, Team);
            Spacecraft.Controller = new PlayerController(Spacecraft);
            Stats.Fuselage = new()
            {
                Make = Makes.Fuselages.Criterion,
                Traits = new()
                {

                },
                Tech = Zones.Current.Index,
                Integrity = 1,
                Velocity = new()
                {
                    Maximum = 5,
                    Minimum = 5,
                    Acceleration = 5,
                    Momentum = 5,
                },
                Rotation = new()
                {
                    MinMax = 5,
                    Acceleration = 5,
                    Momentum = 5,
                },
            };
            Stats.Weapon = new()
            {
                Make = Makes.Weapons.Cannon,
                Traits = new()
                {
                    //Traits.Weapons.MULTISHOT
                },
                Count = 2,
                Tech = Zones.Current.Index,
                Damage = 2,
                Accuracy = 8,
                FireRate = 6,
                Velocity = 7,
            };
            Spacecraft.Fuselage.Defense.OnHit += () => Hit?.Invoke();
        }
        public static Spacecraft Spacecraft { get; private set; }
        public static class Stats
        {
            public static event Action OnFuselageChange;
            public static event Action OnWeaponChange;
            private static void FuselageChange()
            {
                Spacecraft.Generate(_fuselage);
                OnFuselageChange?.Invoke();
            }
            private static void WeaponChange()
            {
                Spacecraft.Generate(_weapon);
                OnWeaponChange?.Invoke();
            }
            private static FuselageStats _fuselage;
            public static FuselageStats Fuselage
            {
                get => _fuselage;
                set
                {
                    if (!_fuselage.Equals(value))
                    {
                        _fuselage = value;
                        FuselageChange();
                    }
                }
            }
            private static WeaponStats _weapon;
            public static WeaponStats Weapon
            {
                get => _weapon;
                set
                {
                    if (!_weapon.Equals(value))
                    {
                        _weapon = value;
                        WeaponChange();
                    }
                }
            }
        }
    }
    public static class Collection
    {
        private static Dictionary<Collectable.RecordDefinition, uint> _recordCounts;
        private static Dictionary<Collectable.RecordDefinition, uint> RecordCounts
        {
            get
            {
                if (_recordCounts is null)
                {
                    _recordCounts = new Dictionary<Collectable.RecordDefinition, uint>();
                    foreach (Collectable.RecordDefinition record in Collectables.Records.All)
                    {
                        _recordCounts.Add(record, 0);
                    }
                }
                return _recordCounts;
            }
        }
        public delegate void ItemCollectedDelegate(Collectable.RecordDefinition record);
        public static event ItemCollectedDelegate ItemCollected;
        public static void Add(Collectable.RecordDefinition record)
        {
            if (RecordCounts.ContainsKey(record))
            {
                uint count = RecordCounts[record];
                count++;
                RecordCounts[record] = count;
                ItemCollected?.Invoke(record);
            }
            else
            {
                throw new Exception("Collectible not found!");
            }
        }
        public static uint Get(Collectable.RecordDefinition record)
        {
            if (RecordCounts.ContainsKey(record))
            {
                return RecordCounts[record];
            }
            return 0;
        }
    }
    public static class Zones
    {
        public static class Current
        {
            public static class Summaries
            {
                private static HashSet<ZoneSummary> ZoneSummaries { get; } = new HashSet<ZoneSummary>();
                public static void Add(SummaryDefinition summary)
                {
                    ZoneSummary zoneSummaries = Get();
                    if (zoneSummaries is null)
                    {
                        zoneSummaries = new ZoneSummary(Index);
                        ZoneSummaries.Add(zoneSummaries);
                    }
                    zoneSummaries.Add(summary);
                }
                public static ZoneSummary Get() => Get(Index);
                public static ZoneSummary Get(uint zoneIndex) => ZoneSummaries.SingleOrDefault(zr => zr.ZoneIndex == zoneIndex);
            }
            private static uint _index;
            public static uint Index
            {
                get => _index;
                set
                {
                    if (_index != value)
                    {
                        _index = value;
                        _index = Math.Max(_index, 1);
                    }
                }
            }
        }
    }
}
