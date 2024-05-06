namespace LootBoot.Epilogue.Engine;
public static class ActorInterrogator
{
    public static List<Actor> Interrogate()
    {
        List<Actor> actors = new List<Actor>();
        foreach (var scene in GameCore.Scenes)
        {
            var sceneActors = Interrogate(scene);
            if (sceneActors is not null)
            {
                actors.AddRange(sceneActors);
            }
        }
        return actors;
    }
    public static List<Actor> Interrogate(ScriptCollection scriptCollection)
    {
        var results = GameInterrogator.Interrogate(scriptCollection);
        List<HashSet<Script>> unsortedActors = results.Raw.Where(ts => Reflecting.IsOrDerivedFrom(ts.Key, typeof(Actor))).Select(ts => ts.Value).ToList();
        if (unsortedActors is not null)
        {
            List<Actor> actors = new List<Actor>();
            foreach (HashSet<Script> scripts in unsortedActors)
            {
                actors.AddRange(scripts.Cast<Actor>().ToList());
            }
            return actors;
        }
        return null;
    }
}
