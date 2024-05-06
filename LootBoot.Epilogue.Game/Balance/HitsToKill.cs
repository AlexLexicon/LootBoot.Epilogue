namespace LootBoot.Epilogue.Game.Balance;
public static class HitsToKill
{
    public static int[][] Chart = new[]
    {
        new[] { 3, 3, 2, 2, 2, 1, 1, 1, 1 },
        new[] { 3, 3, 3, 2, 2, 2, 1, 1, 1 },
        new[] { 4, 3, 3, 3, 2, 2, 2, 1, 1 },
        new[] { 4, 4, 3, 3, 3, 2, 2, 2, 1 },
        new[] { 5, 4, 4, 3, 3, 3, 2, 2, 2 },
        new[] { 5, 5, 4, 4, 3, 3, 3, 2, 2 },
        new[] { 5, 5, 5, 4, 4, 3, 3, 3, 2 },
        new[] { 6, 5, 5, 5, 4, 4, 3, 3, 3 },
        new[] { 6, 5, 5, 5, 4, 4, 4, 3, 3 },
    };
    public static int Get(WeaponStats weaponStats, FuselageStats fuselageStats)
    {
        int chartValue = Chart[fuselageStats.Integrity - 1][weaponStats.Damage - 1];
        int techDiffrence = fuselageStats.Tech - weaponStats.Tech;
        return Math.Max(chartValue + techDiffrence, 1);
    }
}
