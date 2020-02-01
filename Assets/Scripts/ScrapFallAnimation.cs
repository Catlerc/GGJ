using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrapFallAnimation : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float time;

    void Update()
    {
        var vector = Vector3.Lerp(startPos, endPos, time / 2);
        vector.y = Mathf.Sin(time / 2 * Mathf.PI) * 4f;
        transform.position = vector;
        time += Time.deltaTime;
        transform.Rotate(new Vector3((float) Math.Sin(time * Mathf.PI) * Mathf.Rad2Deg * 0.3f,
            (float) Math.Sin(time * Mathf.PI) * Mathf.Rad2Deg * 0.3f,
            (float) Math.Cos(time * Mathf.PI * 1.3f) * Mathf.Rad2Deg * 0.3f));
        if (time > 2)
        {
            transform.rotation = Quaternion.identity;
            var pos = transform.position += Random.insideUnitSphere / 3f;
            pos.y = 0;
            transform.position = pos;
            Destroy(this);
        }
    }
}