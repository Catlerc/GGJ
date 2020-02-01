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
    public BabkaAnimation babk;
    public GameObject hpstat;
    public GameObject sveto;
    void Start()
    {
        cameraWorkPos = cameraObj.transform.position;
        cameraWorkRotate = cameraObj.transform.rotation.eulerAngles;

        ratingDictionary = getAllRating();

        goStartPos();

        canvasStatsObj.SetActive(false);
    }

    private Dictionary<string, int> getAllRating()
    {
        var dict = new Dictionary<string, int>();
        var userIndex = 0;
        while (PlayerPrefs.HasKey($"user{userIndex}Score"))
        {
            dict.Add(PlayerPrefs.GetString($"user{userIndex}Name"), PlayerPrefs.GetInt($"user{userIndex}Score"));
            userIndex++;
        }

        return dict;
    }

    private void saveAllRating(Dictionary<string, int> dict)
    {
        var userIndex = 0;
        foreach (var pair in dict)
        {
            var userName = pair.Key;
            var userScore = pair.Value;
            PlayerPrefs.SetString($"user{userIndex}Name", userName);
            PlayerPrefs.SetInt($"user{userIndex}Score", userScore);
            userIndex++;
        }

        PlayerPrefs.Save();
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
        babk.work = true;
      
        carInstance.MoveCamera();
        Camera.main.transform.position-=Vector3.back*5;
        hpstat.SetActive(true);
        sveto.SetActive(true);
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
        saveAllRating(ratingDictionary);
        ViewRating();
    }

    void ViewRating()
    {
        canvasStatsObj.SetActive(true);

        textRating.text = "";

        foreach (var item in ratingDictionary.OrderBy(i => -i.Value))
        {
            textRating.text += item.Key + ": " + item.Value + "\n";
        }
    }
}