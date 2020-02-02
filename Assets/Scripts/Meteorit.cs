﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteorit : MonoBehaviour
{
    public GameObject holePrefab;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 rotation;
    public Vector2Int endPosInt;
    public float time;
    public float maxRotateSpeed = 10;
    
    void Start()
    {
        rotation = new Vector3(Random.Range(0, maxRotateSpeed), Random.Range(0, maxRotateSpeed), Random.Range(0, maxRotateSpeed));   
    }

    void Update()
    {
        var vector = Vector3.Lerp(startPos, endPos, time);
        transform.position = vector;
        time += Time.deltaTime;
        if (time >= 1)
        {
            bool okay = true;
            for (var x = 0; x < 4; x++)
            for (var y = 0; y < 4; y++)
            {
                var pos = endPosInt + Vector2Int.up * x + Vector2Int.right * y;
                if (Map.Test(pos))
                {
                    if (Map.Get(pos).name.Contains("BigHole"))
                    {
                        okay = false;
                    }
                }
            }

            if (okay)
            {
                var entity = Instantiate(holePrefab, endPos, Quaternion.identity).GetComponent<Entity>();
                for (var x = 0; x < 4; x++)
                for (var y = 0; y < 4; y++)
                {
                    Map.RemoveStatic(endPosInt + Vector2Int.up * x + Vector2Int.right * y);
                    Map.Set(endPosInt + Vector2Int.up * x + Vector2Int.right * y, entity);
                }
            }

            Destroy(gameObject);
        }

        transform.Rotate(rotation);
    }
}