using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRunner : MonoBehaviour
{
    public float speed = 2;

    public float time = 0;

    void Start()
    {
        speed *= 1 + (Random.Range(0, 70) * 1f / 100f);
        Destroy(gameObject, 60);
    }

    void Update()
    {
        time += Time.deltaTime;
        transform.Translate(0, Mathf.Sin(time * Mathf.PI * 5)/20, Time.deltaTime * speed);
    }
}