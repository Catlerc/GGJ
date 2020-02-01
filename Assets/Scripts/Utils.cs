using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class Utils
{
    public static Entity InstantiateToMap(this GameObject prefab, Vector2Int pos)
    {
        var entity = Object.Instantiate(prefab,
                new Vector3(pos.x * Generator.SizeOfTileSide, 0, pos.y * Generator.SizeOfTileSide), Quaternion.identity)
            .GetComponent<Entity>();
        if (entity == null) Debug.LogWarning("Spawn non Entity!");
        else
            Map.Set(pos, entity);
        return entity;
    }

    public static Entity InstantiateToMap(this GameObject prefab, Vector2Int pos, float rotation)
    {
        var entity = Object.Instantiate(prefab,
                new Vector3(pos.x * Generator.SizeOfTileSide, 0, pos.y * Generator.SizeOfTileSide), Quaternion.identity)
            .GetComponent<Entity>();
        entity.transform.rotation = Quaternion.Euler(0, rotation, 0);
        if (entity == null) Debug.LogWarning("Spawn non Entity!");
        else
            Map.Set(pos, entity);
        return entity;
    }

    public static TB[] map<TA, TB>(this TA[] array, Func<TA, TB> func)
    {
        var newArray = new TB[array.Length];
        for (var i = 0; i < array.Length; i++)
            newArray[i] = func(array[i]);
        return newArray;
    }

    public static TB[] map<TA, TB>(this TA[] array, Func<TA, int, TB> func)
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

    public static void map<TA>(this TA[] array, Action<TA, int> func)
    {
        var i = 0;
        foreach (var item in array)
        {
            func(item, i);
            i++;
        }
    }

    public static T PickRandom<T>(this T[] array) => array[Random.Range(0, array.Length)];

    public static T PickByNoise<T>(this T[] array, Vector2Int pos)
    {
        var maxNoiseValue = 0f;
        var maxObj = array[0];
        for (var i = 0; i < array.Length; i++)
        {
            var scale = 15f - i * 3;
            var value = Mathf.PerlinNoise(pos.x / scale + 100 + i, pos.y / scale + 100);
            if (value > maxNoiseValue)
            {
                maxNoiseValue = value;
                maxObj = array[i];
            }
        }

        return maxObj;
    }
}

public static class Repeat
{
    public static void Func(Action<int> func, int count)
    {
        for (var i = 0; i < count; i++)
            func(i);
    }
}