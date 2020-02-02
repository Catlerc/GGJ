using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRunner : MonoBehaviour
{
    public float speed = 2;
    
    void Start()
    {
        speed *= 1 + (Random.Range(0, 70) * 1f / 100f);
        Destroy(gameObject, 60);
    }

    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
