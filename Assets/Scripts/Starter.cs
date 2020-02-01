using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public GameObject cameraObj;
    public Vector3 cameraStartPos, cameraStartRotate;
    private Vector3 cameraWorkPos, cameraWorkRotate;
    public Car carInstance;
    public InputField inputNickname;
    public String nickname;
    public Text textRating;
    public GameObject canvasStartObj;
    public GameObject canvasStatsObj;
    public Dictionary<String, int> ratingDictionary;

    void Start()
    {
        cameraWorkPos = cameraObj.transform.position;
        cameraWorkRotate = cameraObj.transform.rotation.eulerAngles;
        
        ratingDictionary = new Dictionary<string, int>();

        goStartPos();
        
        canvasStatsObj.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            StartGame();
            carInstance.MoveCamera();
        }
    }

    public void StartGame()
    {
        canvasStartObj.SetActive(false);
        canvasStatsObj.SetActive(false);
        
        nickname = inputNickname.text;

        if (nickname == "")
        {
            nickname = "No Name";
        }
        
        goWorkPos();
        carInstance.engine = true;
    }

    public void EndGame()
    {
        carInstance.engine = false;
        AddRating();
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
    
    void AddRating()
    {
        if (ratingDictionary.ContainsKey(nickname))
        {
            if (ratingDictionary[nickname] < carInstance.score)
            {
                ratingDictionary[nickname] = carInstance.score;
            }
        }
        else
            ratingDictionary.Add(nickname, carInstance.score);
        
        ViewRating();
    }

    void ViewRating()
    {
        canvasStatsObj.SetActive(true);
        
        textRating.text = "";
        
        foreach (var item in ratingDictionary.OrderBy(i => i.Value))
        {
            textRating.text += item.Key + " - " + item.Value + "\n";
        }
    }
}
