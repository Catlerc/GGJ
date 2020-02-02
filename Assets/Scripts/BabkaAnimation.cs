using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabkaAnimation : MonoBehaviour
{
    public float time = 0;
    public Vector3 startPos;
    public GameObject p;
    private Vector3 endPos;
    public bool work = false;
    public GameObject warn;
    public bool spw=true;
    public void Start()
    {
        startPos = transform.position;
        endPos = p.transform.position;
    }

    void Update()
    {
        if (work)
        {
            time += Time.deltaTime;
            if (spw)
            {
                spw = false;
                Instantiate(warn,transform.position+Vector3.up*3,Quaternion.identity);
            }
            
            if (time>1)
            {
            var v = Vector3.Lerp(startPos, endPos, time-1);
            v.y = endPos.y + Mathf.Sin(time * Mathf.PI*10)/2;
            transform.position = v;
            
            if (time > 2)
            {
                GetComponentInParent<Car>().engine = true;
                Destroy(this);
            }}
        }
    }
}