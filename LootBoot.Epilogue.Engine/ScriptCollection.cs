namespace LootBoot.Epilogue.Engine;
public abstract class ScriptCollection : GameObject
{
    public ScriptCollection(GameSequencer.Permanence permanence) : base(permanence) => ScriptToName = new PairedDictionary<Script, string>();
    public TScript GetParent<TScript>(bool ascending = true) where TScript : Script => GetParents<TScript>(Source.Parent, ascending, true).FirstOrDefault();
    public List<TScript> GetParents<TScript>(bool ascending = true) where TScript : Script => GetParents<TScript>(Source.Parent, ascending, false);
    public HashSet<TScript> GetScripts<TScript>() where TScript : Script => SelectScriptsQuery<TScript>().ToHashSet();
    public TScript GetScript<TScript>() where TScript : Script => SelectScriptsQuery<TScript>().FirstOrDefault();
    public TScript GetScript<TScript>(string name) where TScript : Script
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }
        if (ScriptToName.ContainsValue(name))
        {
            return ScriptToName.TryGetKey(name, out HashSet<Script> keys) ? (TScript)keys.First() : throw new Exception("Script to name dictionary name mismatch");
        }
        PropertyInfo[] properties = Source.GetType().GetProperties();
        for (int index = 0; index < properties.Length; index++)
        {
            if (Strings.Compare(properties[index].Name, name))
            {
                object propertyValue = properties[index].GetValue(Source);
                if (propertyValue == null)
                {
                    return null;
                }
                Type propertyType = propertyValue.GetType();
                Type scriptType = typeof(TScript);
                if (Reflecting.IsOrDerivedFrom(propertyType, scriptType))
                {
                    foreach (TScript script in SelectScriptsQuery<TScript>())
                    {
                        if (script == propertyValue)
                        {
                            ScriptToName.Add(script, name);
                            return script;
                        }
                    }
                }
            }
        }
        throw new Exception("Script to name dictionary script mismatch");
    }
    internal virtual void Add(Script script)
    {
        if (script is not null)
        {
            script.OnDestroy += () => Remove(script);
            ScriptToName.Add(script, null);
        }
    }
    internal virtual void Remove(Script script)
    {
        if (script is not null)
        {
            ScriptToName.Remove(script);
        }
    }
    private IEnumerable<KeyValuePair<Script, string>> WhereScriptsQuery<TScript>() where TScript : Script => Source.ScriptToName.Where(kvp => Reflecting.IsOrDerivedFrom(kvp.Key?.GetType(), typeof(TScript)));
    private IEnumerable<TScript> SelectScriptsQuery<TScript>() where TScript : Script => WhereScriptsQuery<TScript>().Select(kvp => (TScript)kvp.Key);
    private List<TScript> GetParents<TScript>(Script script, bool ascending, bool first, List<TScript> parents = null) where TScript : Script
    {
        if (parents is null)
        {
            parents = new List<TScript>();
        }
        if (script is null)
        {
            if (parents.Count > 1 && !ascending)
            {
                parents.Reverse();
            }
            return parents;
        }
        if (Reflecting.IsOrDerivedFrom(script?.GetType(), typeof(TScript)))
        {
            parents.Add((TScript)script);
            if (ascending && first)
            {
                return parents;
            }
        }
        return GetParents(script.Parent, ascending, first, parents);
    }
    public Script Source { get; internal set; }
    internal PairedDictionary<Script, string> ScriptToName { get; }
}
