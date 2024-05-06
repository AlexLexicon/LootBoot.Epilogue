namespace LootBoot.Epilogue.Game.Entities;
public class Astroid : Entity
{
    public enum Sizes
    {
        Large,
        Medium,
        Small,
    }
    private const int SCRAP_SPAWN_MAX = 500;
    private const int SIZE_SMALL_MIN = 25;
    private const int SIZE_SMALL_MAX = 50;
    private const int SIZE_MEDIUM_MIN = 75;
    private const int SIZE_MEDIUM_MAX = 100;
    private const int SIZE_LARGE_MIN = 125;
    private const int SIZE_LARGE_MAX = 150;
    private const double ANGLE_CHANGE_MIN = 15;
    private const double ANGLE_CHANGE_MAX = 45;
    private static Size GetSize(Sizes size)
    {
        double diameter = size switch
        {
            Sizes.Large => Rng.ToInteger(SIZE_LARGE_MIN, SIZE_LARGE_MAX),
            Sizes.Medium => Rng.ToInteger(SIZE_MEDIUM_MIN, SIZE_MEDIUM_MAX),
            _ => Rng.ToInteger(SIZE_SMALL_MIN, SIZE_SMALL_MAX),
        };
        return new Size(diameter, diameter);
    }
    private static HashSet<Astroid> LIVING_ASTROIDS { get; } = new HashSet<Astroid>();
    private static int TotalScrapSpawned { get; set; }
    public static void ResetScrapSpawned() => TotalScrapSpawned = 0;
    public static int LivingAstroidsCount => LIVING_ASTROIDS.Count;
    private readonly Point? _spawnPosition;
    private readonly Angle? _spawnAngle;
    private Sizes? _spawnSize;
    public Astroid(Scene scene) : this(scene, null, null, null) { }
    public Astroid(Scene scene, Point? spawnPosition, Angle? spawnAngle, Sizes? spawnSize) : base(scene)
    {
        LIVING_ASTROIDS.Add(this);
        _spawnPosition = spawnPosition;
        _spawnAngle = spawnAngle;
        _spawnSize = spawnSize;
        Sprite = new Texture(this, Geometry)
        {
            ZIndex = (int)Configurations.Sprites.ZIndex.Astroids,
            ImageFilePaths = new()
            {
                "sprites.astroid.15-17.00-00-00.png",
            },
        };
        Defense = new Defense(this, Neutral.Team, Geometry)
        {
            IsMissable = false
        };
        Defense.OnKill += Killed;
        Controller = new AstroidController(this);
    }
    protected override void Create()
    {
        base.Create();
        Rect screenBounds = Viewport.Geometry.Bounds.Screen;
        if (_spawnSize is null)
        {
            _spawnSize = Rng.SelectItem(Sizes.Large, Sizes.Medium); //Rng.ToEnum<Sizes>();
        }
        Size = _spawnSize.Value;
        Size size = GetSize(Size);
        Geometry.Size = size;
        Point position;
        if (_spawnPosition is null)
        {
            position = screenBounds.Rng().Outside(size).ToWorld();
        }
        else
        {
            position = _spawnPosition.Value;
        }
        Geometry.Position = position;
        Angle angle;
        if (_spawnAngle is null)
        {
            Point targetPosition = screenBounds.Rng().Inside().ToWorld();
            angle = position.AngleToTarget(targetPosition);
        }
        else
        {
            angle = _spawnAngle.Value;
        }
        Geometry.Angle = angle;
        Controller.Start<AstroidBehavior>();
    }
    protected virtual void Killed()
    {
        double angle = Defense.LatestAttacker.CollisionGeometry.Angle;
        if (TotalScrapSpawned < SCRAP_SPAWN_MAX)
        {
            int totalScrapToSpawn = Generate.Items.Scraps.RandomCountForAstroid(Size);
            if (TotalScrapSpawned + totalScrapToSpawn > SCRAP_SPAWN_MAX)
            {
                totalScrapToSpawn = SCRAP_SPAWN_MAX - TotalScrapSpawned;
            }
            Generate.Items.Scraps.Spawn(totalScrapToSpawn, Parent, Geometry.Bounds.World, angle);
            TotalScrapSpawned += totalScrapToSpawn;
        }
        Sizes descendantSize = Size switch
        {
            Sizes.Large => Sizes.Medium,
            _ => Sizes.Small,
        };
        int descendantCount = Size switch
        {
            Sizes.Large => 2,
            Sizes.Medium => 3,
            _ => 0,
        };
        Point position = Geometry.Position;
        angle = Defense.LatestAttacker.CollisionGeometry.Angle;
        double angleIncrease = angle;
        double angleDecrease = angle;
        for (int count = 0; count < descendantCount; count++)
        {
            new Astroid(Parent, position, angle, descendantSize);
            angle = calculateNextAngle(count, angleIncrease, angleDecrease);
        }
        Size size = Geometry.Size;
        PVSize pvSize = size.ToPV();
        new Explosion(Parent, position, pvSize);
        static double calculateNextAngle(int count, double angleIncrease, double angleDecrease)
        {
            double angleChange = Rng.ToDouble(ANGLE_CHANGE_MIN, ANGLE_CHANGE_MAX);
            return count % 2 == 0 ? (angleIncrease += angleChange) : (angleDecrease -= angleChange);
        }
    }
    protected override void Destroy()
    {
        LIVING_ASTROIDS.Remove(this);
        base.Destroy();
    }
    public new Scene Parent => (Scene)base.Parent;
    public Sizes Size { get; private set; }
    public Texture Sprite { get; }
    public Defense Defense { get; }
    //private const double SPAWN_DELAY_SECONDS_MIN = 1;
    //private const double SPAWN_DELAY_SECONDS_MAX = 3;
    //private const int SIZE_SMALL_MIN = 25;
    //private const int SIZE_SMALL_MAX = 50;
    //private const int SIZE_MEDIUM_MIN = 75;
    //private const int SIZE_MEDIUM_MAX = 100;
    //private const int SIZE_LARGE_MIN = 125;
    //private const int SIZE_LARGE_MAX = 150;
    //private const double ANGLE_CHANGE_MIN = 15;
    //private const double ANGLE_CHANGE_MAX = 45;
    //public static double GetSize(Sizes size) => size switch
    //{
    //    Sizes.Large => Rng.ToInteger(SIZE_LARGE_MIN, SIZE_LARGE_MAX),
    //    Sizes.Medium => Rng.ToInteger(SIZE_MEDIUM_MIN, SIZE_MEDIUM_MAX),
    //    _ => Rng.ToInteger(SIZE_SMALL_MIN, SIZE_SMALL_MAX),
    //};
    //public static Sizes GetDescendantSize(Sizes size) => size switch
    //{
    //    Sizes.Large => Sizes.Medium,
    //    _ => Sizes.Small,
    //};
    //public static int GetDescendantsCount(Sizes size) => size switch
    //{
    //    Sizes.Large => 2,
    //    Sizes.Medium => 3,
    //    _ => 0,
    //};
    //private DateTime? spawnTime;
    //private readonly AstroidSpawn? spawn;
    //public Astroid(Script parent, Sizes size) : this(parent, null) => Size = size;
    //public Astroid(Script parent, AstroidSpawn? spawn) : base(parent)
    //{
    //    this.spawn = spawn;
    //    Sprite = new Texture(this, Geometry)
    //    {
    //        ZIndex = (int)Configurations.Sprites.ZIndex.Astroids,
    //        ImageFilePaths = new()
    //        {
    //            "cosmic.rock.25-37.0.0.0.png",
    //        },
    //    };
    //    Defense = new Defense(this, Neutral.Team, Geometry);
    //    Controller = new Controller(this);
    //}
    //protected virtual void Spawn()
    //{
    //    SpawnDelaySeconds = null;
    //    double size;
    //    if (spawn is not null)
    //    {
    //        Geometry.Position = spawn.Value.Position;
    //        Geometry.Angle = spawn.Value.Angle;
    //        size = GetSize(spawn.Value.Size);
    //    }
    //    else
    //    {
    //        size = GetSize(Size);
    //    }
    //    Geometry.Size = new Size(size, size);
    //    Controller.Start<AstroidBehavior>();
    //}
    //protected override void OnCreate()
    //{
    //    SpawnDelaySeconds = Rng.ToDouble(SPAWN_DELAY_SECONDS_MIN, SPAWN_DELAY_SECONDS_MAX);
    //    base.OnCreate();
    //}
    //protected override void OnUpdate()
    //{
    //    if (SpawnDelaySeconds is not null && Calculations.DateTime.IsSeconds(ref spawnTime, SpawnDelaySeconds.Value))
    //    {
    //        Spawn();
    //    }
    //    base.OnUpdate();
    //}
    //protected override void OnDestroy()
    //{
    //    if (Defense.IsKilled)
    //    {
    //        Sizes descendantSize = GetDescendantSize(Size);
    //        double angle = Defense.LatestAttacker.CollisionGeometry.Angle;
    //        double angleIncrease = angle;
    //        double angleDecrease = angle;
    //        for (int index = 0; index < GetDescendantsCount(Size); index++)
    //        {
    //            new Astroid(Parent, new AstroidSpawn()
    //            {
    //                Position = Geometry.Position,
    //                Size = descendantSize,
    //                Angle = new Angle(angle)
    //            });
    //            double angleChange = Rng.ToDouble(ANGLE_CHANGE_MIN, ANGLE_CHANGE_MAX);
    //            angle = index % 2 == 0 ? (angleIncrease += angleChange) : (angleDecrease -= angleChange);
    //        }
    //    }
    //    base.OnDestroy();
    //}
    //public Texture Sprite { get; }
    //public Defense Defense { get; }
    //public bool IsDescendant => spawn is not null;
    //public double? SpawnDelaySeconds { get; protected set; }
    //public Sizes Size { get; protected set; }
    //public struct AstroidSpawn
    //{
    //    public Point Position { get; init; }
    //    public Sizes Size { get; init; }
    //    public Angle Angle { get; init; }
    //}
}
