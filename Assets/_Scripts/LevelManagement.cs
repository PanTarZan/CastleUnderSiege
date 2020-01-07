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
    
    public bool isGamePaused = false;
    

    public GameObject PausedScreen;

    // Start is called before the first frame update
    void Start()
    {
        HQ = gameObject.GetComponent<HeadQuaters>();
        CameraRaycaster = FindObjectOfType<CameraRaycaster>();
        DM = gameObject.GetComponent<DialogManager>();
        gameObject.GetComponent<LevelManagement>().enabled = false;
        DM.enabled = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPausedState();
        }

        CheckIfGameIsPaused();
    }

    public void SwitchPausedState()
    {
        isGamePaused = !isGamePaused;
    }

    private void CheckIfGameIsPaused()
    {

        if (isGamePaused)
        {
            Time.timeScale = 0;
            PausedScreen.SetActive(isGamePaused);
        }
        else
        {
            Time.timeScale = 1;
            PausedScreen.SetActive(isGamePaused);
        }
    }
}
