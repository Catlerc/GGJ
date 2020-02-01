using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Utils
{
    public static Entity InstantiateToMap(this GameObject prefab, Vector2Int pos)
    {
        var entity = Object.Instantiate(prefab, new Vector3(pos.x*Generator.SizeOfTileSide, 0, pos.y*Generator.SizeOfTileSide), Quaternion.identity)
            .GetComponent<Entity>();
        if (entity == null) Debug.LogWarning("Spawn non Entity!");
        else
            global::Map.Set(pos, entity);
        return entity;
    }
    
    public static Entity InstantiateToMap(this GameObject prefab, Vector2Int pos, float rotation)
    {
        var entity = Object.Instantiate(prefab, new Vector3(pos.x*Generator.SizeOfTileSide, 0, pos.y*Generator.SizeOfTileSide), Quaternion.identity)
            .GetComponent<Entity>();
        entity.transform.rotation = Quaternion.Euler(0,rotation,0);
        if (entity == null) Debug.LogWarning("Spawn non Entity!");
        else
            global::Map.Set(pos, entity);
        return entity;
    }

    public static TB[] map<TA, TB>(this TA[] array, Func<TA, TB> func)
    {
        var newArray = new TB[array.Length];
        for (var i = 0; i < array.Length; i++)
            newArray[i] = func(array[i]);
        return newArray;
    }

    public static TB[] imap<TA, TB>(this TA[] array, Func<TA, int, TB> func)
    {
        var newArray = new TB[array.Length];
        for (var i = 0; i < array.Length; i++)
            newArray[i] = func(array[i], i);
        return newArray;
    }

    public static void map<TA>(this TA[] array, Action<TA> func)
    {
        foreach (var item in array)
            func(item);
    }

    public static void imap<TA>(this TA[] array, Action<TA, int> func)
    {
        var i = 0;
        foreach (var item in array)
        {
            func(item, i);
            i++;
        }
    }
}