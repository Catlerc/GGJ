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
    public int score;

    void Start()
    {
        engine = false;
        score = 0;
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

    void EndGame()
    {
        engine = false;
        GetComponent<Starter>().EndGame();
    }

    void Move()
    {
        var newAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed + transform.rotation.eulerAngles.y;
        if (!(newAngle > 90 && newAngle < 270))
            transform.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));
        transform.Translate(Vector3.forward * (Time.deltaTime * forwardSpeed));
    }

    void CheckRoad()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100))
        {
            print(hit.collider);
            if (hit.collider.CompareTag("Border"))
            {
                print("Hit");
                print(hit.collider);
                EndGame();
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
        EndGame();
    }

}
