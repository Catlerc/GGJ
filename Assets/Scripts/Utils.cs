using UnityEngine;

public static class Utils
{
    public static void InstantiateToMap(this GameObject prefab, Vector2Int pos)
    {
        var entity = Object.Instantiate(prefab).GetComponent<Entity>();
        if (entity == null) Debug.LogWarning("Spawn non Entity!");
        else
            Map.Set(pos, entity);
    }
}