namespace LootBoot.Epilogue.Game.Characteristics;
public abstract class TraitsCollection<TTraits> : IEnumerable<TTraits> where TTraits : Enum
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<TTraits> GetEnumerator() => TraitsDictionary.Values.GetEnumerator();
    public TraitsCollection() => TraitsDictionary = new Dictionary<Rarity, TTraits>();
    public void Add(TTraits trait)
    {
        Rarity rarity = (Rarity)(object)trait;
        if (TraitsDictionary.ContainsKey(rarity))
        {
            TraitsDictionary[rarity] = trait;
        }
        else
        {
            TraitsDictionary.Add(rarity, trait);
        }
    }
    public TTraits this[Rarity rarity] => TraitsDictionary[rarity];
    private Dictionary<Rarity, TTraits> TraitsDictionary { get; }
}
