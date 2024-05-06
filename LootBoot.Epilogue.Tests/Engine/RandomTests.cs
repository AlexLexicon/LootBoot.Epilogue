using LootBoot.Epilogue.Engine;
using NUnit.Framework;
using System.Collections.Generic;

namespace LootBoot.Epilogue.Tests.Engine;
public class RandomTests
{
    enum Pers
    {
        Remainder,
        TenPercent,
        ThirdyPercent,
        FiftyPercent,
        TooMuch,
    }
    [Test]
    public void RandomChance()
    {
        //Pers r = Rng.Percentage.Chances(Pers.Remainder, (Pers.TenPercent, 0.10), (Pers.ThirdyPercent, 0.30), (Pers.FiftyPercent, 0.50), (Pers.TooMuch, 0.50));
        int attempts = 10000;
        Dictionary<Pers, int> results = new Dictionary<Pers, int>();
        for (int count = 0; count < attempts; count++)
        {
            Pers r = Rng.Percentage.Chances(Pers.Remainder, (Pers.FiftyPercent, 0.50), (Pers.ThirdyPercent, 0.30), (Pers.TenPercent, 0.10));
            if (results.ContainsKey(r))
            {
                results[r] = results[r] + 1;
            }
            else
            {
                results.Add(r, 1);
            }
        }
    }
}
