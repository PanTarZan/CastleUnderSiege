using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagement : MonoBehaviour
{
    public float volume;


    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveSettings(Slider slider)
    {
        foreach (var a in FindObjectsOfType<AudioListener>())
        {
            AudioListener.volume = slider.value;
        }
    }
}


