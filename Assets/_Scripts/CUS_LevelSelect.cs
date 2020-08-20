using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CUS_LevelSelect : MonoBehaviour
{
    public Camera m_camera;
    public Button levelStartButton;
    public Account currentAccount;

    public Vector3 cameraOffset = new Vector3(0,3,0);
    public GameObject menuCamPos = null;
    public CUS_LevelMarker[] levelMarkers = new CUS_LevelMarker[0];
    public int currentScreenNumber = 1;

    public GameObject IntroSceneSetup;

    public UnityEvent onLevelSwitch;

    // Start is called before the first frame update
    void Start()
    {
        levelMarkers = GetComponentsInChildren<CUS_LevelMarker>();
        currentAccount = FindObjectOfType<Account>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraLocationOnPoint();
        HandleUnlockedLevels();
    }

    private void HandleUnlockedLevels()
    {
        if (currentScreenNumber > currentAccount.levelsUnlocked || currentScreenNumber == 0)
        {
            levelStartButton.interactable = false;
        }
        else
        {
            levelStartButton.interactable = true;
        }
    }

    public void SwitchToNextLevel()
    {
        currentScreenNumber += 1;
        if (currentScreenNumber > levelMarkers.Length)
        {
            currentScreenNumber = 1;
        }
        onLevelSwitch.Invoke();
    }
    public void SwitchToPrevLevel()
    {
        currentScreenNumber -= 1;
        if (currentScreenNumber < 1)
        {
            currentScreenNumber = levelMarkers.Length;
        }
        onLevelSwitch.Invoke();
    }
    public void SwitchToMenu()
    {
        currentScreenNumber = 0;
        onLevelSwitch.Invoke();
    }

    public void SetCameraLocationOnPoint()
    {
        m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, CurrentScreenPosition().position, Time.deltaTime);
        m_camera.transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, CurrentScreenPosition().rotation, Time.deltaTime);
    }

    private Transform CurrentScreenPosition()
    {
        for (int i = 0; i < levelMarkers.Length; i++)
        {
            if (levelMarkers[i].levelNumber == currentScreenNumber)
            {
                return levelMarkers[i].transform;
            }
        }
        return menuCamPos.transform;

    }
}
