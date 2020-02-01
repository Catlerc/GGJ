using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float rotateSpeed;
    public float forwardSpeed;
    public GameObject cameraObj;
    public float cameraOffsetZ;
    public bool engine = true;

    void Start()
    {
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
        var newAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed + transform.rotation.eulerAngles.y;
        if (!(newAngle > 90 && newAngle < 270))
            transform.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));
        //transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0);
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
    }

    void CheckRoad()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100))
        {
            Debug.Log(hit.collider);
            if (hit.collider.CompareTag("Border"))
            {
                engine = false;
            }
        }
    }

    void MoveCamera()
    {
        var oldPos = cameraObj.transform.position;

        cameraObj.transform.position = new Vector3(oldPos.x, oldPos.y, transform.position.z + cameraOffsetZ);
    }
}