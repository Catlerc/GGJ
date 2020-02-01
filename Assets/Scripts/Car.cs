using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float rotateSpeed;
    public float forwardSpeed;
    public GameObject cameraObj;
    public float cameraSpeed;
    public float cameraMaxOffset;
    public bool engine;

    void Start()
    {
        engine = false;
    }

    void Update()
    {
        if (engine)
        {
            Move();
            CheckRoad();
            MoveCamera();
        }
    }

    void Move()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0);
        transform.Translate(Vector3.forward * (Time.deltaTime * forwardSpeed));
    }

    void CheckRoad()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100))
        {
            if (hit.collider.CompareTag("Border"))
            {
                print("Hit");
                print(hit.collider);
                engine = false;
            }
        }
    }

    public void MoveCamera()
    {
        var pos = cameraObj.transform.position;
        
        pos.z += Time.deltaTime * cameraSpeed;

        if (transform.position.z > pos.z - cameraMaxOffset)
        {
            pos.z = transform.position.z + cameraMaxOffset;
        }
        
        cameraObj.transform.position = pos;
    }

    private void OnBecameInvisible()
    {
        engine = false;
    }
}
