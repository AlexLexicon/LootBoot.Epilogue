namespace LootBoot.Epilogue.Game.Scenes.Battles;
public class Battle1 : Battle
{
    public override List<Wave> GenerateWaves()
    {
        return new()
        {
            new Wave(this)
            {
                new WaveDefinition<Spacecraft, Spacecraft.Definition>
                {
                    GenerateDefinition = Spacecrafts.Wasp,
                    Team = Enemy.Team,
                    Count = 1,
                }
            },
            new Wave(this)
            {
                new WaveDefinition<Spacecraft, Spacecraft.Definition>
                {
                    GenerateDefinition = Spacecrafts.Wasp,
                    Team = Enemy.Team,
                    Count = 2,
                }
            },
            new Wave(this)
            {
                new WaveDefinition<Spacecraft, Spacecraft.Definition>
                {
                    GenerateDefinition = Spacecrafts.Wasp,
                    Team = Enemy.Team,
                    Count = 3,
                }
            }
            //new Wave(this)
            //{
            //    GetTest(1),
            //    //GetTest(2),
            //    //GetTest(3),
            //    //GetTest(4),
            //    //GetTest(5),
            //    //GetTest(6),
            //    //GetTest(7),
            //    //GetTest(8),
            //    //GetTest(9),
            //},
            //new Wave(this)
            //{
            //    new WaveDefinition<Spacecraft, Spacecraft.Definition>
            //    {
            //        GenerateDefinition = Spacecrafts.Wasp,
            //        Team = Enemy.Team,
            //        Count = 10,
            //    }
            //}
        };
        //WaveDefinition GetTest(uint integrityTeir) => new WaveDefinition<Spacecraft, Spacecraft.Definition>
        //{
        //    GenerateDefinition = new Spacecraft.Definition
        //    {
        //        FuselageStats = new Characteristics.Fuselages.FuselageStats
        //        {
        //            //PVSizeWidthScale = Spacecrafts.Wasp.FuselageStats.PVSizeWidthScale,
        //            Make = Spacecrafts.Wasp.FuselageStats.Make,
        //            Integrity = integrityTeir,
        //            Tech = Spacecrafts.Wasp.FuselageStats.Tech,
        //            Traits = Spacecrafts.Wasp.FuselageStats.Traits,
        //            Rotation = Spacecrafts.Wasp.FuselageStats.Rotation,
        //            Velocity = Spacecrafts.Wasp.FuselageStats.Velocity,
        //        },
        //        WeaponStats = Spacecrafts.Wasp.WeaponStats,
        //        GetController = (sc) =>
        //        {
        //            TestController testController = new TestController(sc);
        //            return testController;
        //        },
        //    },
        //    Team = Enemy.Team,
        //    Count = 1,
        //};
    }
    //public class TestController : Controller
    //{
    //    public TestController(Script parent) : base(parent)
    //    {
    //        Start<TestBehavior>();
    //    }
    //    public class TestBehavior : ArtificialStrategyBehavior
    //    {
    //        int ins_pos;
    //        static int pos;
    //        protected override void OnCreate()
    //        {
    //            base.OnCreate();
    //            Point center = Viewport.Geometry.ToCenter();
    //            Size size = Geometry.Size;
    //            Geometry.Position = new Point(center.X + pos * size.Width, center.Y);
    //            ins_pos = pos;
    //            pos++;
    //        }
    //        protected override void OnUpdate()
    //        {
    //            if (ins_pos is 0)
    //            {
    //                //needs to only happen once!
    //                if (Player.Actor.Spacecraft is not null)
    //                {
    //                    uint damage = GetDamageFromInput(out bool doDamage);
    //                    int tech = GetTechFromInput(out bool doTech);
    //                    if (doTech || doDamage)
    //                    {
    //                        SetWeaponStats(damage, tech);
    //                    }
    //                }
    //                void SetWeaponStats(uint damage, int tech) => Player.Actor.Stats.Weapon = new Characteristics.Weapons.WeaponStats
    //                {
    //                    //PVSizeWidthScale = Player.Actor.Stats.Weapon.PVSizeWidthScale,
    //                    Count = Player.Actor.Stats.Weapon.Count,
    //                    Make = Player.Actor.Stats.Weapon.Make,
    //                    Traits = Player.Actor.Stats.Weapon.Traits,
    //                    Tech = tech,
    //                    Damage = damage,
    //                    Accuracy = Player.Actor.Stats.Weapon.Accuracy,
    //                    FireRate = Player.Actor.Stats.Weapon.FireRate,
    //                    Velocity = Player.Actor.Stats.Weapon.Velocity,
    //                };
    //                int GetTechFromInput(out bool doTech)
    //                {
    //                    doTech = true;
    //                    int tech = Player.Actor.Stats.Weapon.Tech;
    //                    if (Input.IsKeyReleased(System.Windows.Input.Key.OemMinus))
    //                    {
    //                        return tech - 1;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.OemPlus))
    //                    {
    //                        return tech + 1;
    //                    }
    //                    doTech = false;
    //                    return tech;
    //                }
    //                uint GetDamageFromInput(out bool doDamage)
    //                {
    //                    doDamage = true;
    //                    if (Input.IsKeyReleased(System.Windows.Input.Key.D1))
    //                    {
    //                        return 1;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D2))
    //                    {
    //                        return 2;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D3))
    //                    {
    //                        return 3;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D4))
    //                    {
    //                        return 4;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D5))
    //                    {
    //                        return 5;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D6))
    //                    {
    //                        return 6;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D7))
    //                    {
    //                        return 7;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D8))
    //                    {
    //                        return 8;
    //                    }
    //                    else if (Input.IsKeyReleased(System.Windows.Input.Key.D9))
    //                    {
    //                        return 9;
    //                    }
    //                    doDamage = false;
    //                    return Player.Actor.Stats.Weapon.Damage;
    //                }
    //                base.OnUpdate();
    //            }
    //        }
    //    }
    //}
}
