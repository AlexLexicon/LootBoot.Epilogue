namespace LootBoot.Epilogue.Game.Actors;
public sealed class Spacecraft : WaveFactor<Spacecraft.Definition>
{
    public event Action OnFuselageChange;
    public event Action OnWeaponChange;
    public Spacecraft(Scene scene, GameTeam team) : base(scene, team) { }
    protected override void Destroy()
    {
        WaveStepComplete();
        base.Destroy();
    }
    public override void Generate(Definition definition)
    {
        Generate(definition.FuselageStats);
        Generate(definition.WeaponStats);
        Record = definition.Record;
        Controller = definition.GetController(this);
        CanDropCollectables = definition.CanDropCollectables;
    }
    public void Generate(FuselageStats fuselage) => Fuselage = fuselage.Make switch
    {
        Makes.Fuselages.Angler => new AnglerFuselage(this, Team, fuselage),
        Makes.Fuselages.Beetle => new BeetleFuselage(this, Team, fuselage),
        Makes.Fuselages.Criterion => new CriterionFuselage(this, Team, fuselage),
        Makes.Fuselages.Eel => new EelFuselage(this, Team, fuselage),
        Makes.Fuselages.Firefly => new FireflyFuselage(this, Team, fuselage),
        Makes.Fuselages.Gnat => new GnatFuselage(this, Team, fuselage),
        Makes.Fuselages.Mosquito => new MosquitoFuselage(this, Team, fuselage),
        Makes.Fuselages.Parasite => new ParasiteFuselage(this, Team, fuselage),
        Makes.Fuselages.Spider => new SpiderFuselage(this, Team, fuselage),
        Makes.Fuselages.Wasp => new WaspFuselage(this, Team, fuselage),
        _ => null,
    };
    public void Generate(WeaponStats weapon)
    {
        if (Fuselage is null)
        {
            throw new Exception("The Fuselage must be generated before the Weapon can be generated");
        }
        Hardpoint[] hardpoints = Fuselage.Sprites.GetHardpoints((int)weapon.Count);
        Weapon = weapon.Make switch
        {
            Makes.Weapons.Arc => new Arc(this, Team, Fuselage.Geometry, hardpoints, weapon),
            Makes.Weapons.Cannon => new Cannon(this, Team, Fuselage.Geometry, hardpoints, weapon),
            Makes.Weapons.Flamer => new Flamer(this, Team, Fuselage.Geometry, hardpoints, weapon),
            Makes.Weapons.Laser => new Laser(this, Team, Fuselage.Geometry, hardpoints, weapon),
            Makes.Weapons.Minelayer => new Minelayer(this, Team, Fuselage.Geometry, hardpoints, weapon),
            Makes.Weapons.Turret => new Turret(this, Team, Fuselage.Geometry, hardpoints, weapon),
            _ => null,
        };
    }
    public void Kill()
    {
        if (!IsKilled)
        {
            IsKilled = true;
            Controller = new Controller(this);
            Controller.Sequence<KilledBehavior>().Complete().Start<DestroyBehavior>();
            Controller.Start<KilledBehavior>();
            Player.Zones.Current.Summaries.Add(Record);
        }
    }
    public void SetScale(double scale)
    {
        Fuselage.Sprites.SetSizeScale(scale);
        Weapon.Sprites.SetSizeScale(scale);
    }
    private void FuselageChange() => OnFuselageChange?.Invoke();
    private void WeaponChange() => OnWeaponChange?.Invoke();
    public bool IsOccluded
    {
        get
        {
            if (Fuselage is not null && Fuselage.IsOccluded)
            {
                return true;
            }
            if (Weapon is not null && Weapon.IsOccluded)
            {
                return true;
            }
            return false;
        }
    }
    private Fuselage _fuselage;
    public Fuselage Fuselage
    {
        get => _fuselage;
        private set
        {
            if (_fuselage != value)
            {
                if (_fuselage is not null)
                {
                    _fuselage.QueueDestroy();
                }
                _fuselage = value;
                FuselageChange();
            }
        }
    }
    private Weapon _weapon;
    public Weapon Weapon
    {
        get => _weapon;
        private set
        {
            if (_weapon != value)
            {
                _weapon = value;
                WeaponChange();
            }
        }
    }
    public SummaryDefinition Record { get; private set; }
    private Controller _controller;
    public Controller Controller
    {
        get => _controller;
        set
        {
            if (_controller != value)
            {
                if (_controller is not null)
                {
                    _controller.QueueDestroy();
                }
                _controller = value;
            }
        }
    }
    public bool IsKilled { get; private set; }
    public bool CanDropCollectables { get; private set; }
    public struct Definition
    {
        public delegate Controller GetControllerDelegate(Spacecraft parent);
        public GetControllerDelegate GetController { get; init; }
        public bool CanDropCollectables { get; init; }
        public FuselageStats FuselageStats { get; init; }
        public WeaponStats WeaponStats { get; init; }
        public SummaryDefinition Record { get; init; }
    }
}
