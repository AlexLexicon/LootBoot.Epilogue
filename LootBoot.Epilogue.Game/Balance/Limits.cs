namespace LootBoot.Epilogue.Game.Balance;
public static class Limits
{
    public static class Items
    {
        public static ItemLimits Standard { get; } = new()
        {
            Velocity = new()
            {
                Maximum = 300,
                Minimum = 0,
                Acceleration = 25,
                Momentum = 6,
            }
        };
    }
    public static class Modifications
    {
        public static ModificationLimits Standard { get; } = new()
        {

        };
    }
    public static class Fuselages
    {
        public static FuselageLimits Angler { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Beetle { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Criterion { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Eel { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Firefly { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Gnat { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Mosquito { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Parasite { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Spider { get; } = new()
        {
            PVSizeWidthScale = 0.08,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
        public static FuselageLimits Wasp { get; } = new()
        {
            PVSizeWidthScale = 0.05,
            Velocity = new()
            {
                Maximum = (500, 800),
                Minimum = (-300, -100),
                Acceleration = (10, 100),
                Momentum = (1, 75),
            },
            Rotation = new()
            {
                MinMax = (100, 200),
                Acceleration = (5, 25),
                Momentum = (10, 75),
            }
        };
    }
    public static class Weapons
    {
        public static WeaponLimits Arc { get; } = new()
        {
            PVSizeWidthScale = 0.03,
            Accuracy = (0, 10),
            Velocity = (10, 20),
            FireRate = (0.1, 1),
        };
        public static WeaponLimits Cannon { get; } = new()
        {
            PVSizeWidthScale = 0.03,
            Accuracy = (0, 10),
            Velocity = (900, 1200),// (100, 500),//
            FireRate = (0.1, 1),
        };
        public static WeaponLimits Flamer { get; } = new()
        {
            PVSizeWidthScale = 0.03,
            Accuracy = (0, 10),
            Velocity = (10, 20),
            FireRate = (0.1, 1),
        };
        public static WeaponLimits Laser { get; } = new()
        {
            PVSizeWidthScale = 0.03,
            Accuracy = (0, 10),
            Velocity = (10, 20),
            FireRate = (0.1, 1),
        };
        public static WeaponLimits Minelayer { get; } = new()
        {
            PVSizeWidthScale = 0.03,
            Accuracy = (0, 10),
            Velocity = (10, 20),
            FireRate = (0.1, 1),
        };
        public static WeaponLimits Turret { get; } = new()
        {
            PVSizeWidthScale = 0.03,
            Accuracy = (0, 10),
            Velocity = (10, 20),
            FireRate = (0.1, 1),
        };
    }
}
