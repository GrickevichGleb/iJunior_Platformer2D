using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    private const string IsVolumeOn = "IsVolumeOn";
    
    private const string MasterVolumeParam = "MasterVolume";
    private const string ButtonsVolumeParam = "ButtonsVolume";
    private const string BackgroundVolumeParam = "BackgroundVolume";
    
    private const float OnVolume = 0f;
    private const float OffVolume = -80f;
    
    [SerializeField] private AudioMixerGroup _master;
    
    [SerializeField] private GameObject OnIcon;
    [SerializeField] private GameObject OffIcon;

    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _buttonsVolumeSlider;
    [SerializeField] private Slider _backgroundVolumeSlider;
    
    private bool _isVolumeOn = true;

    private void Start()
    {
        _masterVolumeSlider.value = PlayerPrefs.GetFloat(MasterVolumeParam, 1f);
        _buttonsVolumeSlider.value = PlayerPrefs.GetFloat(ButtonsVolumeParam, 1f);
        _backgroundVolumeSlider.value = PlayerPrefs.GetFloat(BackgroundVolumeParam, 1f);

        _isVolumeOn = Convert.ToBoolean(PlayerPrefs.GetInt(IsVolumeOn, 1));
        
        if(!_isVolumeOn)
            ToggleSound();
    }

    public void ToggleSound()
    {
        _isVolumeOn = !_isVolumeOn;

        if(_isVolumeOn)
            _master.audioMixer.SetFloat(MasterVolumeParam, OnVolume);
        else 
            _master.audioMixer.SetFloat(MasterVolumeParam, OffVolume);
        
        OnIcon.SetActive(_isVolumeOn);
        OffIcon.SetActive(!_isVolumeOn);
        
        PlayerPrefs.SetInt(IsVolumeOn, _isVolumeOn ? 1 : 0);
    }

    public void SetMasterVolume(float value)
    {
        _master.audioMixer.SetFloat(MasterVolumeParam, Mathf.Log10(value) * 20f);
        
        PlayerPrefs.SetFloat(MasterVolumeParam, value);
    }

    public void SetButtonsVolume(float value)
    {
        _master.audioMixer.SetFloat(ButtonsVolumeParam, Mathf.Log10(value) * 20f);
        
        PlayerPrefs.SetFloat(ButtonsVolumeParam, value);
    }

    public void SetBackgroundVolume(float value)
    {
        _master.audioMixer.SetFloat(BackgroundVolumeParam, Mathf.Log10(value) * 20f);
        
        PlayerPrefs.SetFloat(BackgroundVolumeParam, value);
    }
}
