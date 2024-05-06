namespace LootBoot.Epilogue.Game.Balance;
public static class Generate
{
    public static class Items
    {
        public static class Modules
        {
            public static void Spawn(Scene scene, Point spawnPosition)
            {
                Collectable.Definition? definition = Random();
                if (definition is not null)
                {
                    new Collectable(scene, definition.Value, spawnPosition, 0);
                }
            }
            public static Collectable.Definition? Random()
            {
                const int NOTHING_RESULT = 0;
                const int RARE_RESULT = 1;
                const int REMARKABLE_RESULT = 2;
                return Rng.Percentage.Chances(NOTHING_RESULT, (RARE_RESULT, 0.30), (REMARKABLE_RESULT, 0.10)) switch
                {
                    RARE_RESULT => Collectables.RareModule,
                    REMARKABLE_RESULT => Collectables.RemarkableModule,
                    _ => null,
                };
            }
        }
        public static class Scraps
        {
            public static void Spawn(int total, Scene scene, Rect regionToSpawnInside, Angle angle)
            {
                if (total <= 0)
                {
                    return;
                }
                const double ANGLE_CHANGE_MIN = 15;
                const double ANGLE_CHANGE_MAX = 45;
                double angleIncrease = angle;
                double angleDecrease = angle;
                for (int count = 0; count < total; count++)
                {
                    Point spawnPosition = regionToSpawnInside.Rng().Inside();
                    new Collectable(scene, Collectables.Scrap, spawnPosition, angle);
                    angle = calculateNextAngle(count, angleIncrease, angleDecrease);
                }
                static double calculateNextAngle(int count, double angleIncrease, double angleDecrease)
                {
                    double angleChange = Rng.ToDouble(ANGLE_CHANGE_MIN, ANGLE_CHANGE_MAX);
                    return count % 2 == 0 ? (angleIncrease += angleChange) : (angleDecrease -= angleChange);
                }
            }
            public static int RandomCount()
            {
                bool hasAny = Rng.Percentage.Chance(0.40);
                return hasAny ? Rng.ToInteger(5, 15) : 0;
            }
            public static int RandomCountForAstroid(Astroid.Sizes size)
            {
                (bool hasAny, int min, int max) = size switch
                {
                    Astroid.Sizes.Large => (Rng.Percentage.Chance(0.65), 1, 5),
                    Astroid.Sizes.Medium => (Rng.Percentage.Chance(0.75), 5, 10),
                    _ => (Rng.Percentage.Chance(0.85), 15, 30),
                };
                return hasAny ? Rng.ToInteger(min, max) : 0;
            }
        }
    }
}
