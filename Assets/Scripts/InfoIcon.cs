using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoIcon : MonoBehaviour
{
    public float lifetime = 1;
    public float speedUp = 1;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(0, Time.deltaTime * speedUp, 0);
    }
}
