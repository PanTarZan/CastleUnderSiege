using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;

public class CUS_SettingSystem : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Toggle enablePostProcessing;
    public Toggle toggle_2;
    public float master = -40f;
    public float music = -40f;
    public float effects;
    public AudioMixerGroup masterAM;
    public AudioMixerGroup musicAM;
    public AudioMixerGroup effectsAM;
    public bool enablePostProcessing_value;
    public bool toggle_2_value;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetSettingsToUI();
    }

    // Update is called once per frame
    void Update()
    {
        handlePostProcessing();
    }

    private void SetSettingsToUI()
    {
        Debug.Log("Setting already achieved settings in scene: " + Application.loadedLevelName);
        masterSlider.value = master;
        musicSlider.value = music;
        effectsSlider.value = effects;
        enablePostProcessing.isOn = enablePostProcessing_value;
        toggle_2.isOn = toggle_2_value;

    }


    private void handlePostProcessing()
    {
        if (Camera.main.GetComponent<PostProcessingBehaviour>().enabled != enablePostProcessing_value)
        {
            Camera.main.GetComponent<PostProcessingBehaviour>().enabled = enablePostProcessing_value;
            Debug.Log("PostProcessing changed to: " + enablePostProcessing_value);
        }
    }

    public void SwitchPostProcessing()
    {
        enablePostProcessing_value = enablePostProcessing.isOn;
    }

    public void SetMasterSound(float soundLevel)
    {
        masterAM.audioMixer.SetFloat("MasterValue", soundLevel);
        master = soundLevel;
    }
    public void SetMusicSound(float soundLevel)
    {
        masterAM.audioMixer.SetFloat("MusicValue", soundLevel);
        master = soundLevel;
    }
    public void SetEffectsSound(float soundLevel)
    {
        masterAM.audioMixer.SetFloat("EffectsValue", soundLevel);
        master = soundLevel;
    }
}
