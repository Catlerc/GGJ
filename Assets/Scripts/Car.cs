using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public GameObject[] repairPoint;
    public float rotateSpeed;
    public float forwardSpeed;
    public GameObject cameraObj;
    public float cameraSpeed;
    public float cameraMaxOffset;
    public bool engine;
    private int _score = 0;
    public GameObject smogPrefab;
    public GameObject smog;
    public bool ssfds = true;
    
    public int score
    {
        set
        {
            _score = value;
            text.text = $"Очки: {value}";
            
        }
        get => _score;
    }

    public int _hp = 4;
    public GameObject repairedPrefab;
    public GameObject infoIconPrefab;
    public Generator gen;
    public float time = 0;
    public float maxTime = 60 * 2;
    public RectTransform progressbar;
    public Starter starter;
    public GameObject HPImage1;
    public GameObject HPImage2;
    public GameObject HPImage3;
    public Text text;
    public AudioSource music;
    
    public int hp
    {
        set
        {
            if (engine)
            {
                
                _hp = value;
                switch (value)
                {
                    case 2:
                        Destroy(HPImage3);
                        break;
                    case 1:
                        Destroy(HPImage2);
                        break;
                    case 0:
                        Destroy(HPImage1);
                        starter.GameOver();
                        break;
                }

                Respawn();
            }
        }
        get => _hp;
    }

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (engine)
        {
            if (ssfds)
            {
                ssfds = false;
                smog.SetActive(true);
            }
            Move();
            CheckRoad();
            MoveCamera();
            time += Time.deltaTime;
            progressbar.sizeDelta = new Vector2(778.45f - 778.45f * time / maxTime, 63.39999f);
            if (time > maxTime) starter.EndGame();
        }

        music.pitch = 1 + time / maxTime / 2 ;
  

        repairPoint.map(obj => repair(obj.transform.position));
    }

    public void Respawn() // Будем считать что эта хрень  работает
    {
        var pos = transform.position;
        Destroy(Instantiate(smogPrefab,pos,Quaternion.identity),5);
        bool spawned = false;
        transform.rotation = Quaternion.identity;
        var g = Mathf.FloorToInt(cameraObj.transform.position.z / 0.8f)+7;
        for (var i = 0; i < 13 - 4; i++)
        {
            bool canSpawn = true;
            for (int j = -2; j < 4; j++)
            for (int j2 = 0; j < 14; j++)
                if (Map.Get(new Vector2Int(i + j, g + j2)).CompareTag("dead"))
                {
                    canSpawn = false;
                    break;
                }

            if (canSpawn)
            {
                transform.position = new Vector3((i+1) * 0.8f, pos.y, cameraObj.transform.position.z-3);
                spawned = true;
                break;
            }
        }

        if (!spawned)//https://s.fishki.net/upload/users/2019/12/30/482/696a27c4296dfcdbea055c8c17e702ae.jpg
            for (var i = -13; i < 0; i++)
            {
                bool canSpawn = true;
                for (int j = -2; j <= 4; j++)
                for (int j2 = 0; j < 14; j++)

                    if (Map.Get(new Vector2Int(i + j, g + j2)).CompareTag("dead"))
                    {
                        canSpawn = false;
                        break;
                    }

                if (canSpawn)
                {
                    transform.position = new Vector3((i+1) * 0.8f, pos.y, cameraObj.transform.position.z-3);
                    spawned = true;
                    break;
                }
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
                score += entity.toScore;
            }

            if (hit.collider.CompareTag("scrap"))
            {
                res = true;
                var entity = hit.collider.GetComponent<Entity>();
                var pos = entity.pos;
                Map.RemoveDynamic(pos);
                Instantiate(infoIconPrefab, transform.position, Quaternion.identity);
                score -= 500;
            }
        }

        return res;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dead")) hp -= 1;
    }
}