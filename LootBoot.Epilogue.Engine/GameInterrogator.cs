namespace LootBoot.Epilogue.Engine;
public static class GameInterrogator
{
    public static InterrogationResults Interrogate(ScriptCollection scriptCollection) => new InterrogationResults(scriptCollection, Interrogate(scriptCollection, null));
    private static Dictionary<Type, HashSet<Script>> Interrogate(ScriptCollection scriptCollection, Dictionary<Type, HashSet<Script>> results)
    {
        if (results is null)
        {
            results = new Dictionary<Type, HashSet<Script>>();
        }
        if (scriptCollection is not null)
        {
            HashSet<Script> scripts = scriptCollection.GetScripts<Script>();
            if (scripts is not null)
            {
                foreach (Script script in scripts)
                {
                    if (script is not null)
                    {
                        if (script is ScriptCollection sc)
                        {
                            results = Interrogate(sc, results);
                        }
                        Type scriptType = script.GetType();
                        if (results.ContainsKey(scriptType))
                        {
                            HashSet<Script> scriptsResult = results[scriptType];
                            scriptsResult.Add(script);
                            results[scriptType] = scriptsResult;
                        }
                        else
                        {
                            HashSet<Script> scriptsResult = new()
                            {
                                script,
                            };
                            results.Add(scriptType, scriptsResult);
                        }
                    }
                }
            }
        }
        return results;
    }
    public class InterrogationResults
    {
        public InterrogationResults(ScriptCollection root, Dictionary<Type, HashSet<Script>> raw)
        {
            Root = root;
            Raw = raw;
            if (Raw is not null)
            {
                TotalTypes = Raw.Keys.Count;
                ScriptCounts = new Dictionary<Type, int>();
                foreach (KeyValuePair<Type, HashSet<Script>> TypeToscripts in Raw)
                {
                    int count = TypeToscripts.Value?.Count ?? 0;
                    ScriptCounts.Add(TypeToscripts.Key, count);
                    TotalScripts += count;
                }
            }
        }
        public ScriptCollection Root { get; }
        public int TotalTypes { get; }
        public int TotalScripts { get; }
        public Dictionary<Type, int> ScriptCounts { get; }
        public Dictionary<Type, HashSet<Script>> Raw { get; }
    }
}
