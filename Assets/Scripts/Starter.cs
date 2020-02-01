using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public GameObject cameraObj;
    public Vector3 cameraStartPos, cameraStartRotate;
    private Vector3 cameraWorkPos, cameraWorkRotate;

    void Start()
    {
        cameraWorkPos = cameraObj.transform.position;
        cameraWorkRotate = cameraObj.transform.rotation.eulerAngles;

        goStartPos();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        goWorkPos();
        gameObject.GetComponent<Car>().engine = true;
    }

    void goWorkPos()
    {
        cameraObj.transform.position = cameraWorkPos;
        cameraObj.transform.rotation = Quaternion.Euler(cameraWorkRotate);
    }

    void goStartPos()
    {
        cameraObj.transform.position = cameraStartPos;
        cameraObj.transform.rotation = Quaternion.Euler(cameraStartRotate);
    }
}
