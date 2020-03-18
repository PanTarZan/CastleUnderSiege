﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class CUS_UI_Load_Level_Screen : CUS_UI_Screen
{
    #region variables
    [Header("Timed Screen Properties")]

    public float m_ScreenTime = 0.1f;
    public UnityEvent onTimeCompleted = new UnityEvent();
    public Slider slider;

    private float startTime;
    #endregion

    #region MainMethods
    #endregion

    #region HelperMethods

    public override void StartScreen()
    {
        base.StartScreen();

        startTime = Time.time;
        StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(m_ScreenTime);


        if (onTimeCompleted != null)
        {
            onTimeCompleted.Invoke();
        }
        
    }

    public IEnumerator WaitToLoadScene(int sceneIndex, Slider slider)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            if (slider)
            {
                slider.value = progress;
            }
            yield return null;
        }
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(WaitToLoadScene(sceneIndex, slider));
    }
    #endregion
}
