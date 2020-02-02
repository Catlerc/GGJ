using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class res : MonoBehaviour
{
    public Generator gen;
    public bool canRestart = false;

    void Update()
    {
        if (canRestart &&Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        if ( Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}