using System.Collections;
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
        Debug.Log("Loading");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (slider)
            {
                slider.value = progress;
            }
            yield return null;
        }
    }

    public void LoadSceneFromMenu()
    {
        var sceneIndex = FindObjectOfType<CUS_LevelSelect>().currentScreenNumber;
        Debug.Log("loading scene: "+ sceneIndex);
        StartCoroutine(WaitToLoadScene(sceneIndex, slider));
    }

    public void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("loading scene: " + 0);
            StartCoroutine(WaitToLoadScene(0, slider));
        }
        else
        {
            Debug.Log("loading scene: " + sceneIndex);
            StartCoroutine(WaitToLoadScene(sceneIndex, slider));
        }
    }
    public void LoadMainMenu()
    {
        Debug.Log("loading Main Menu");
        StartCoroutine(WaitToLoadScene(0, slider));
    }
    #endregion
}
