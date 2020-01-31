using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    private static readonly Dictionary<Vector2Int, Entity> StaticMap = new Dictionary<Vector2Int, Entity>();

    public static bool Set(Vector2Int pos, Entity entity)
    {
        if (StaticMap.ContainsKey(pos))
        {
            Debug.LogWarning($"Object {entity.name} can't be spawned on static map.");
            return false;
        }
        else
        {
            StaticMap.Add(pos, entity);
            entity.pos = pos;
            return true;
        }
    }

    public static void Remove(Vector2Int pos) {
        if (StaticMap.TryGetValue(pos, out var entity)) entity.Destroy();
        StaticMap.Remove(pos);
    }
}