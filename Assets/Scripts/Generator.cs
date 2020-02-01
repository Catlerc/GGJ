using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Generator: MonoBehaviour
{
    [NonSerialized]
    public static float SizeOfTileSide = 8 / 10f;
    public GameObject[] prefabs;
    public GameObject bord;
    [NonSerialized]
    public readonly int width = 28;
    public int lineIndex = 0;
    public void InstantiateLine()
    {
        for (var i = 1; i <=width;i++)
        {
            switch (i)
            {
                case 1:
                    bord.InstantiateToMap(new Vector2Int(i,lineIndex),-90);
                    break;
                case int index when index==width:
                    bord.InstantiateToMap(new Vector2Int(i,lineIndex),90);
                    break;
                default:
                    prefabs[Random.Range(0, prefabs.Length)].InstantiateToMap(new Vector2Int(i,lineIndex), 90*Random.Range(0,4));    
                    break;
            }
        }
        lineIndex++;
    }
    
    //TEST 

    public void Start()
    {
        for (int i=0;i<20;i++)
            InstantiateLine();
    }
}