using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Text uiText;
    public float availableTime;

    public Camera fpsCam;
    public Camera overheadCam;

    private float seconds;
    private float minutes;
    private float gameTime;

    private string remainingTime;

    private bool canCount = true;
    private bool doOnce = false;

    private void Update()
    {   
        gameTime = (int)(availableTime - Time.timeSinceLevelLoad);
        seconds = Mathf.CeilToInt(availableTime - Time.timeSinceLevelLoad) % 60;
        minutes = Mathf.CeilToInt(availableTime - Time.timeSinceLevelLoad) / 60;
        remainingTime = string.Format("{0:00} : {1:00}", minutes, seconds);
       
        uiText.text = remainingTime;

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
           // print("correct number");
            ShowOverheadView();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ShowFPSView();
        }
    }

    public void ShowOverheadView()
    {
        fpsCam.enabled = false;
        overheadCam.enabled = true;
    }

    public void ShowFPSView()
    {
        fpsCam.enabled = true;
        overheadCam.enabled = false;
    }
}
