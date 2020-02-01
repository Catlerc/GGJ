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
    public GameObject[] big;
    private readonly int width = 32;
    public int lineIndex = 0;
    public int delLineIndex = 0;
    private int noiseTranslation = 100;

    public void InstantiateLine()
    {
        if (Random.Range(0f, 100f) < lineIndex / 40f)
        {
            var pos = new Vector2Int(Random.Range(-15, 15), lineIndex);

            bool okay = true;
            for (var x = 0; x < 4; x++)
            {
                if (Map.Test(pos + Vector2Int.right * x)) okay = false;
            }

            if (okay)
            {
                var entity = big.PickRandom().InstantiateToMap(pos, 0);
                for (var x = 0; x < 4; x++)
                for (var y = 0; y < 4; y++)
                    if (!(x == 0 && y == 0))
                        Map.Set(pos + Vector2Int.up * x + Vector2Int.right * y, entity);
            }
        }

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

        if (Random.Range(0, 100) < lineIndex / 4f)
            SpawnScrap(new Vector2Int(-30, lineIndex + 10), new Vector2Int(Random.Range(-20, 20), lineIndex));


        lineIndex++;
    }

    public void delLine()
    {
        delLineIndex++;
        for (var x = 0; x <= width / 2 + 5; x++)
            Repeat.Func(c =>
            {
                c = c == 0 ? 1 : -1;
                Map.Remove(new Vector2Int(c * x, delLineIndex));
            }, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, 10));
    }

    public void Start()
    {
        for (int i = 0; i < 45; i++)
            InstantiateLine();
        SpawnScrap(new Vector2Int(-30, 0), new Vector2Int(-5, 0));
    }

    public void SpawnScrap(Vector2Int startPos, Vector2Int endPos)
    {
        var entity = scrap.PickRandom().InstantiateToMap(endPos);
        var anim = entity.gameObject.GetComponent<ScrapFallAnimation>();
        anim.startPos = new Vector3(startPos.x * 0.8f, 0, startPos.y * 0.8f);
        anim.endPos = new Vector3(endPos.x * 0.8f, .01f, endPos.y * 0.8f);
    }
}