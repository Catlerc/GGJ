using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public Generator gen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gen.lineIndex = 0;
            gen.delLineIndex = 0;
            print("aDASDA?!");
            SceneManager.LoadScene(0);
            
        }
    }
}
