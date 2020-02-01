using UnityEngine;
using UnityEngine.Serialization;

public class Generator: MonoBehaviour
{ 
    public GameObject tilePrefab;
    public GameObject brokenPrefab;
    public readonly int width = 28;
    public int lineIndex = 0;
    public GameObject[] GetNewLine()
    {
        var array = new GameObject[width].imap((_,i) =>
        {
            switch (Random.Range(1, 2))
            {
                case 1:
                    return tilePrefab;
                case 2:
                    return brokenPrefab;
            }
            Debug.LogWarning("Null prefab on map.");
            return null;
        });
        return array;
    }


}