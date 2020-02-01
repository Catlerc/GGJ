using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    [NonSerialized] public static float SizeOfTileSide = 8 / 10f;
    public GameObject[] prefabs;
    public GameObject bord;
    private readonly int width = 28;
    private int lineIndex = 0;

    private bool isBroken(Vector2Int pos)
    {
        float scale = 1f / 12;
        float v = Mathf.PerlinNoise(pos.x * scale, pos.y * scale);

        float scale2 = 1f / 10;
        float v2 = Mathf.PerlinNoise(pos.x * scale2, pos.y * scale2);

        return v > .5f || v2 > .5f;
    }

    public void InstantiateLine()
    {
        for (var x = 1; x <= width; x++)
        {
            switch (x)
            {
                case 1:
                    bord.InstantiateToMap(new Vector2Int(x, lineIndex), -90);
                    break;
                case int index when index == width:
                    bord.InstantiateToMap(new Vector2Int(x, lineIndex), 90);
                    break;
                default:
                    prefabs[isBroken(new Vector2Int(x, lineIndex)) ? 0 : 1]
                        .InstantiateToMap(new Vector2Int(x, lineIndex), 90 * Random.Range(0, 4));
                    break;
            }
        }

        lineIndex++;
    }

    //TEST 

    public void Start()
    {
        for (int i = 0; i < 100; i++)
            InstantiateLine();
    }
}