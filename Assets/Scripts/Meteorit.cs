using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteorit : MonoBehaviour
{
    public GameObject holePrefab;
    public GameObject smogPrefab;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 rotation;
    public Vector2Int endPosInt;
    public float time;
    public float maxRotateSpeed = 10;

    void Start()
    {
        rotation = new Vector3(Random.Range(0, maxRotateSpeed), Random.Range(0, maxRotateSpeed),
            Random.Range(0, maxRotateSpeed));
    }

    void Update()
    {
        var vector = Vector3.Lerp(startPos, endPos, time);
        transform.position = vector;
        time += Time.deltaTime;
        if (time >= 1)
        {
            bool okay = true;
            for (var x = -1; x <= 4; x++)
            for (var y = -1; y <= 4; y++)
            {
                var pos = endPosInt + Vector2Int.up * x + Vector2Int.right * y;
                if (Map.Test(pos))
                {
                    var ss = Map.Get(pos);
                    if (ss != null)
                    {
                        if (ss.name.Contains("BigHole"))
                        {
                            okay = false;
                        }
                    }
                }
            }

            if (okay)
            {
                var entity = Instantiate(holePrefab, new Vector3(endPosInt.x * 0.8f, .01f, endPosInt.y * 0.8f), Quaternion.identity).GetComponent<Entity>();
                for (var x = 0; x < 4; x++)
                for (var y = 0; y < 4; y++)
                {
                    Map.Remove(endPosInt + Vector2Int.up * x + Vector2Int.right * y);
                    Map.Set(endPosInt + Vector2Int.up * x + Vector2Int.right * y, entity);
                }
            }
            
            Destroy(Instantiate(smogPrefab, endPos, Quaternion.identity), 10);

            Destroy(gameObject);
        }

        transform.Rotate(rotation);
    }
}