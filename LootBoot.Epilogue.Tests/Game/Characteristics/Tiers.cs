using LootBoot.Epilogue.Game;
using LootBoot.Epilogue.Game.Characteristics;
using NUnit.Framework;

namespace LootBoot.Epilogue.Tests.Game.Characteristics;
public class Tiers
{
    [Test]
    public void Run()
    {
        //test when a limit matches!
        DoubleLimit matching = (20, 20);
        DoubleLimit limits = (1, 100);
        Tier tier = 0;

        double ascending = tier.GetAscending(limits);
        double descending = tier.GetDescending(limits);

        double matchascending = tier.GetAscending(matching);
        double matchdescending = tier.GetDescending(matching);
    }
    [Test]
    public void Items()
    {

    }
}
