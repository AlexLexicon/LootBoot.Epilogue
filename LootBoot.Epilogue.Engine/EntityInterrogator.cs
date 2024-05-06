namespace LootBoot.Epilogue.Engine;
public static class EntityInterrogator
{
    public static List<Entity> Interrogate()
    {
        List<Entity> entities = new List<Entity>();
        foreach (var scene in GameCore.Scenes)
        {
            var sceneEntities = Interrogate(scene);
            if (sceneEntities is not null)
            {
                entities.AddRange(sceneEntities);
            }
        }
        return entities;
    }
    public static List<Entity> Interrogate(ScriptCollection scriptCollection)
    {
        var results = GameInterrogator.Interrogate(scriptCollection);
        List<HashSet<Script>> unsortedEntities = results.Raw.Where(ts => Reflecting.IsOrDerivedFrom(ts.Key, typeof(Entity))).Select(ts => ts.Value).ToList();
        if (unsortedEntities is not null)
        {
            List<Entity> entities = new List<Entity>();
            foreach (HashSet<Script> scripts in unsortedEntities)
            {
                entities.AddRange(scripts.Cast<Entity>().ToList());
            }
            return entities;
        }
        return null;
    }
}
