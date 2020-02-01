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
    public GameObject[] scrap;
    private readonly int width = 32;
    public int lineIndex = 0;
    public int delLineIndex = 0;
    private int _noiseTranslation = 100;

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
                            grassPrefabs.PickRandom()
                                .InstantiateToMap(new Vector2Int(x * c, lineIndex), 90 * Random.Range(0, 4));
                            break;
                    }
                }, x == 0 ? 1 : 2);
        }
        lineIndex++;
    }

    public void delLine()
    {
        delLineIndex++;
        for (var x = 0; x <= width / 2 + 5; x++)
        {
            Repeat.Func(c =>
            {
                c = c == 0 ? 1 : -1;
                Map.Remove(new Vector2Int(c * x, delLineIndex));
            }, 2);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, 10));
    }
    //TEST 

    public void Start()
    {
        for (int i = 0; i < 40; i++)
            InstantiateLine();
        SpawnScrap(new Vector2Int(-30, 0), new Vector2Int(-5, 0));
    }

    public void SpawnScrap(Vector2Int startPos, Vector2Int endPos)
    {
        var entity = scrap.PickRandom().InstantiateToMap(endPos);
        var anim = entity.gameObject.GetComponent<ScrapFallAnimation>();
        anim.startPos = new Vector3(startPos.x * 0.8f, 0, startPos.y * 0.8f);
        anim.endPos = new Vector3(endPos.x * 0.8f, 0, endPos.y * 0.8f);
    }
}