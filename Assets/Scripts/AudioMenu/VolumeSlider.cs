using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private const float MinSliderValue = 0.0001f;
    private const float MaxSliderValue = 1f;
    
    [SerializeField] private AudioMixerGroup _master;
    [SerializeField] private VolumeParameters _volumeParameter;

    [SerializeField] private Slider _slider;
    
    private void Start()
    {
        _slider.onValueChanged.AddListener(SetVolume);
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter.ToString(), MaxSliderValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        float volume = Mathf.Clamp(value, MinSliderValue, MaxSliderValue);
        
        _master.audioMixer.SetFloat(_volumeParameter.ToString(), Mathf.Log10(volume) * 20f);
        
        PlayerPrefs.SetFloat(_volumeParameter.ToString(), value);
    }
}
