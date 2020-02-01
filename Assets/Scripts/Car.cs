using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject[] repairPoint;
    public float rotateSpeed;
    public float forwardSpeed;
    public GameObject cameraObj;
    public float cameraSpeed;
    public float cameraMaxOffset;
    public bool engine;
    public int score;
    public int _hp = 3;
    public GameObject repairedPrefab;
    public Generator gen;

    public int hp
    {
        set
        {
            _hp = value;
            print("-HP");
            Respawn();
        }
        get => _hp;
    }

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
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

        if (!GetComponent<Renderer>().isVisible)
        {
            hp -= 1;
            print("!!!!!ASD");
        }


        repairPoint.map(obj => repair(obj.transform.position));
    }

    public void Respawn()
    {
        var pos = transform.position;
        transform.position = new Vector3(0, pos.y, cameraObj.transform.position.z);
    }

    void EndGame()
    {
        engine = false;
        GetComponent<Starter>().EndGame();
    }

    void Move()
    {
        var newAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed + transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));
        transform.Translate(Vector3.forward * (Time.deltaTime * forwardSpeed));
    }

    void CheckRoad()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100))
        {
            if (hit.collider.CompareTag("Border")) hp -= 1;
        }
    }

    public void MoveCamera()
    {
        var pos = cameraObj.transform.position;
        pos.z += Time.deltaTime * cameraSpeed;
        if (transform.position.z > pos.z - cameraMaxOffset)
            pos.z = transform.position.z + cameraMaxOffset;
        else
            pos.z += Time.deltaTime * 3;
        for (float i = gen.lineIndex * 0.8f; i <= Mathf.FloorToInt(transform.position.z) + 16; i++)
        {
            gen.InstantiateLine();
            gen.delLine();
        }
        cameraObj.transform.position = pos;
    }

    public bool repair(Vector3 posArg)
    {
        bool res = false;
        RaycastHit hit;
        if (Physics.Raycast(posArg, Vector3.down, out hit, 10))
        {
            if (hit.collider.CompareTag("Repairable1"))
            {
                res = true;
                var entity = hit.collider.GetComponent<Entity>();
                var pos = entity.pos;
                var realpos = entity.transform.position;
                Map.RemoveStatic(pos);
                var newEntity = Instantiate(repairedPrefab, realpos, Quaternion.identity).GetComponent<Entity>();
                Map.Set(pos, newEntity);
            }
            if (hit.collider.CompareTag("scrap"))
            {
                res = true;
                var entity = hit.collider.GetComponent<Entity>();
                var pos = entity.pos;
                var realpos = entity.transform.position;
                Map.RemoveDynamic(pos);
                print("SCRAP");
            }
        }
        return res;
    }
}
