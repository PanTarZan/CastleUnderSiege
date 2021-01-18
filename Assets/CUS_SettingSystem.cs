using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;

public class CUS_SettingSystem : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider otherSlider;
    public Toggle enablePostProcessing;
    public Toggle toggle_2;
    public float music = -40f;
    public float effects;
    public float other;
    public AudioMixerGroup musicAM;
    public AudioMixerGroup effectsAM;
    public AudioMixerGroup otherAM;
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
        handleAudioMixersProcessing();
    }

    private void handleAudioMixersProcessing()
    {

    }

    private void SetSettingsToUI()
    {
        musicSlider.value = music;
        effectsSlider.value = effects;
        otherSlider.value = other;
        enablePostProcessing.isOn = enablePostProcessing_value;
        toggle_2.isOn = toggle_2_value;

    }


    private void handlePostProcessing()
    {
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = enablePostProcessing_value;
    }

    public void SwitchPostProcessing()
    {
        enablePostProcessing_value = enablePostProcessing.isOn;
    }

    public void SetMusicSound(float soundLevel)
    {
        musicAM.audioMixer.SetFloat("MusicValue", soundLevel);
        music = soundLevel;
    }
}
