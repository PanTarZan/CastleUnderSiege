using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    //private HeadQuaters HQ;
    //private CameraRaycaster CameraRaycaster;
    //private DialogManager DM;

    public CUS_UI_Screen VictoryScreen;
    public CUS_UI_Screen GameOverScreen;
    public CUS_UI_Screen PausedScreen;
    public CUS_UI_Screen gameUI;
    public UnityEvent onVictory;
    public UnityEvent onGameOver;


    public bool isGamePaused = false;
    public bool hasGameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        //HQ = gameObject.GetComponent<HeadQuaters>();
        //CameraRaycaster = FindObjectOfType<CameraRaycaster>();
        //DM = gameObject.GetComponent<DialogManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
                PauseGame();
        }

        ManageCursorState();
    }

    private void ManageCursorState()
    {
        if (isGamePaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ShowVictoryScreen()
    {
        if (!hasGameEnded)
        {
            isGamePaused = true;
            hasGameEnded = true;
            FindObjectOfType<CUS_UI_System>().SwitchScreens(VictoryScreen);
            onVictory.Invoke();
            if (FindObjectOfType<Account>())
            {
                Debug.Log("Account Found");
                var acc = FindObjectOfType<Account>();
                acc.levelsUnlocked += 1;
                //CUS_Save_system.SaveAccountData(acc);
            }
        }
    }

    public void ShowGameOverScreen()
    {
        if (!hasGameEnded)
        {
            hasGameEnded = true;
            isGamePaused = true;
            FindObjectOfType<CUS_UI_System>().SwitchScreens(GameOverScreen);
            onGameOver.Invoke();
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
        isGamePaused = false;
    }

    public void TurnOnGameOver()
    {
        ShowGameOverScreen();
    }
}
