namespace LootBoot.Epilogue.Game.Characteristics.Modifications;
public class ModificationAttributes : Attributes<ModificationLimits, ModificationStats>
{
    public ModificationAttributes(Script parent, ModificationStats stats, ModificationLimits limits) : base(parent, stats, limits) { }
}
