using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    private static readonly Dictionary<Vector2Int, GameObject> StaticMap = new Dictionary<Vector2Int, GameObject>();
    private static readonly Dictionary<Vector2Int, GameObject> DynamicMap = new Dictionary<Vector2Int, GameObject>();

    public static bool Set(Vector2Int pos, Entity entity)
    {
        if (entity.isStatic)
        {
            if (StaticMap.ContainsKey(pos))
            {
                
//                Debug.LogWarning($"Object {entity.name} can't be spawned on static map.");
                entity.Destroy();
                return false;
            }
            else
            {
                StaticMap.Add(pos, entity.gameObject);
                entity.pos = pos;
                return true;
            }
        }
        else
        {
            if (DynamicMap.ContainsKey(pos))
            {
                //Debug.LogWarning($"Object {entity.name} can't be spawned on dynamic map.");
                return false;
            }
            else
            {
                DynamicMap.Add(pos, entity.gameObject);
                entity.pos = pos;
                return true;
            }
            
        }
        
    }

    public static void Remove(Vector2Int pos)
    {
        
        if (StaticMap.TryGetValue(pos, out var entity1)) Object.Destroy(entity1);
        StaticMap.Remove(pos);
        if (DynamicMap.TryGetValue(pos, out var entity2)) Object.Destroy(entity2);
        DynamicMap.Remove(pos);
    }
    public static void RemoveDynamic(Vector2Int pos)
    {
        if (DynamicMap.TryGetValue(pos, out var entity2)) Object.Destroy(entity2);
        DynamicMap.Remove(pos);
    }
    public static void RemoveStatic(Vector2Int pos)
    {
        if (StaticMap.TryGetValue(pos, out var entity1)) Object.Destroy(entity1);
        StaticMap.Remove(pos);
    }

    public static bool Test(Vector2Int pos)
    {
        return StaticMap.ContainsKey(pos);
    }
    public static GameObject Get(Vector2Int pos)
    {
        return StaticMap[pos];
    }


}