using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CUS_UI_System : MonoBehaviour
{
    #region variables
    [Header("Main Properties")]
    public CUS_UI_Screen m_StartScreen;

    [Header("System Events")]
    public UnityEvent onSwitchedScreen = new UnityEvent();


    [Header("Fader Properties")]
    public Image m_Fader;
    public float m_FadeInDuration = 1f;
    public float m_FadeOutDuration = 1f;


    public Component[] screens = new Component[0];
    private CUS_UI_Screen currentScreen;
    public CUS_UI_Screen CurrentScreen { get { return currentScreen; } }

    private CUS_UI_Screen previousScreen;
    public CUS_UI_Screen PreviousScreen { get { return previousScreen; } }
    #endregion

    #region MainMethods
    // Start is called before the first frame update
    void Start()
    {
        screens = GetComponentsInChildren<CUS_UI_Screen>(true);
        InitializeScreens();


        if (m_StartScreen)
        {
            SwitchScreens(m_StartScreen);
        }

        if (m_Fader)
        {
            m_Fader.gameObject.SetActive(true);
        }
        FadeIn();
    }
    #endregion

    #region HelperMethods

    public void FadeIn()
    {
        if (m_Fader)
        {
            m_Fader.CrossFadeAlpha(0f, m_FadeInDuration, false);
        }
    }

    public void FadeOut()
    {
        if (m_Fader)
        {
            m_Fader.CrossFadeAlpha(1f, m_FadeOutDuration, false);
        }
    }

    public void SwitchScreens(CUS_UI_Screen aScreen)
    {
        if (aScreen)
        {
            if (CurrentScreen)
            {
                currentScreen.CloseScreen();
                previousScreen = currentScreen;
            }

            currentScreen = aScreen;
            currentScreen.gameObject.SetActive(true);
            currentScreen.StartScreen();

            if (onSwitchedScreen != null)
            {
                onSwitchedScreen.Invoke();
            }
        }

    }

    public void GoToPreviousScreen()
    {
        if (previousScreen)
        {
            SwitchScreens(previousScreen);
        }
    }

    

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    

    void InitializeScreens()
    {
        foreach (var s in screens)
        {
            s.gameObject.SetActive(true);
        }
    }
    #endregion
}
