using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    [NonSerialized] public static float SizeOfTileSide = 8 / 10f;
    public GameObject[] prefabs;
    public GameObject[] grassPrefabs;
    public GameObject bord;
    public GameObject bush;
    private readonly int width = 32;
    private int lineIndex = 0;

    private int noiseTranslation = 100;

    private bool isBroken(Vector2Int pos)
    {
        float scale = 1f / 12;
        float v = Mathf.PerlinNoise(pos.x * scale + noiseTranslation, pos.y * scale + noiseTranslation);

        float scale2 = 1f / 10;
        float v2 = Mathf.PerlinNoise(pos.x * scale2 + noiseTranslation, pos.y * scale2 + noiseTranslation);

        return v > .5f || v2 > .5f;
    }

    public void InstantiateLine()
    {
        for (var x = 0; x <= width / 2 + 5; x++)
        {
            Repeat.Func(c =>
                {
                    c = c == 0 ? 1 : -1;
                    switch (x)
                    {
                        case int n when n < width / 2:
                            var pos = new Vector2Int(c * x, lineIndex);
                            prefabs.PickByNoise(pos).InstantiateToMap(pos, 90 * Random.Range(0, 4));
                                
                            break;
                        case int n when n == width / 2:
                            bord.InstantiateToMap(new Vector2Int(x * c, lineIndex), 90 * c);
                            break;
                        case int n when n <= width / 2 + 2:
                            grassPrefabs.PickRandom().InstantiateToMap(new Vector2Int(x * c, lineIndex),
                                90 * Random.Range(0, 4));
                            break;
                        default:
                            grassPrefabs.PickRandom().InstantiateToMap(new Vector2Int(x * c, lineIndex), 90 * Random.Range(0, 4));
                            break;
                    }
                }, x == 0 ? 1 : 2);
        }

        lineIndex++;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, 10));
    }
    //TEST 

    public void Start()
    {
        for (int i = 0; i < 200; i++)
            InstantiateLine();
    }
}