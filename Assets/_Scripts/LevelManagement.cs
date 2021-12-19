﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    //private HeadQuaters HQ;
    //private CameraRaycaster CameraRaycaster;
    //private DialogManager DM;

    public CUS_UI_Screen VictoryScreen;
    public CUS_UI_Screen GameOverScreen;
    public CUS_UI_Screen PausedScreen;
    public CUS_UI_Screen upgradeScreen;
    public CUS_UI_Screen gameUI;
    public UnityEvent onVictory;
    public UnityEvent onGameOver;
    public int levelUnlockedIndex = 0;


    public bool isGamePaused = false;
    public bool hasGameEnded = false;
    private HeadQuaters HQ;

    // Start is called before the first frame update
    void Start()
    {
        HQ = gameObject.GetComponent<HeadQuaters>();
        //CameraRaycaster = FindObjectOfType<CameraRaycaster>();
        //DM = gameObject.GetComponent<DialogManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGamePaused = true;
                PauseGame();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGamePaused = true;
                UpgradeScreen();
            }
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
                var acc = FindObjectOfType<CurrentPlayerAccount>();
                if (acc.levelsUnlocked <= levelUnlockedIndex)
                {
                    acc.levelsUnlocked = levelUnlockedIndex;
                    acc.gold = HQ.currentMoney;
                    CUS_Save_system.SaveAccountData(acc, Application.persistentDataPath+"/"+acc.AccountName+".kappa");
                }
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
        //isGamePaused = true;
        Time.timeScale = 0;
        FindObjectOfType<CUS_UI_System>().SwitchScreens(PausedScreen);
    }
    public void UpgradeScreen()
    {
        //isGamePaused = true;
        Time.timeScale = 0;
        FindObjectOfType<CUS_UI_System>().SwitchScreens(upgradeScreen);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<CUS_UI_System>().SwitchScreens(gameUI);
        isGamePaused = false;
    }

    public void TurnOnGameOver()
    {
        Debug.Log("level manager game over");
        ShowGameOverScreen();
    }
}
