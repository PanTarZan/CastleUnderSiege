using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CUS_LevelSelect : MonoBehaviour
{
    public Camera m_camera;
    public Vector3 cameraOffset = new Vector3(0,3,0);
    public GameObject menuCamPos = null;
    public CUS_LevelMarker[] levelMarkers = new CUS_LevelMarker[0];
    public int currentScreenNumber = 1;

    public UnityEvent onLevelSwitch;

    // Start is called before the first frame update
    void Start()
    {
        levelMarkers = GetComponentsInChildren<CUS_LevelMarker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToNextLevel()
    {
        //TODO and so on :)
        currentScreenNumber += 1;
        if (currentScreenNumber > levelMarkers.Length)
        {
            currentScreenNumber = 1;
        }
        onLevelSwitch.Invoke();
    }
    public void SwitchToPrevLevel()
    {
        //TODO and so on :)
        currentScreenNumber -= 1;
        if (currentScreenNumber < 1)
        {
            currentScreenNumber = levelMarkers.Length;
        }
        onLevelSwitch.Invoke();
    }
    public void SwitchToMenu()
    {
        //TODO and so on :)
        currentScreenNumber = 0;
        onLevelSwitch.Invoke();
    }

    public void SetCameraLocationOnPoint()
    {
        m_camera.transform.position = CurrentScreenPosition().position + cameraOffset;
        m_camera.transform.LookAt(CurrentScreenPosition());
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
