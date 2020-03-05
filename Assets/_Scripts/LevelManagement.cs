using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    private HeadQuaters HQ;
    private CameraRaycaster CameraRaycaster;
    private DialogManager DM;

    public CUS_UI_Screen VictoryScreen;
    public CUS_UI_Screen GameOverScreen;
    public CUS_UI_Screen PausedScreen;
    public CUS_UI_Screen gameUI;


    public bool isGamePaused = false;
    public bool hasGameEnded = false;



    public AudioClip StageMusic;

    // Start is called before the first frame update
    void Start()
    {
        HQ = gameObject.GetComponent<HeadQuaters>();
        CameraRaycaster = FindObjectOfType<CameraRaycaster>();
        DM = gameObject.GetComponent<DialogManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed");
            PauseGame();
        }
    }



    public void ShowVictoryScreen()
    {
        if (!hasGameEnded)
        {
            FindObjectOfType<CUS_UI_System>().SwitchScreens(VictoryScreen);
            hasGameEnded = true;
        }
    }

    public void ShowGameOverScreen()
    {
        if (!hasGameEnded)
        {
            FindObjectOfType<CUS_UI_System>().SwitchScreens(GameOverScreen);
            hasGameEnded = true;
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        FindObjectOfType<CUS_UI_System>().SwitchScreens(PausedScreen);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<CUS_UI_System>().GoToPreviousScreen();
    }

    public void TurnOnGameOver()
    {
        ShowGameOverScreen();
        FindObjectOfType<Shooting>().enabled = false;
    }
}
