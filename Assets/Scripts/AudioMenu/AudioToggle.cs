using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    private const string IsVolumeOn = "IsVolumeOn";

    private const float OnVolume = 0f;
    private const float OffVolume = -80f;
    
    [SerializeField] private Image OnIcon;
    [SerializeField] private Image OffIcon;

    [SerializeField] private AudioMixerGroup _master;
    [SerializeField] private VolumeParameters _volumeParameter;
    
    private bool _isVolumeOn = true;

    private void Start()
    {
        _isVolumeOn = Convert.ToBoolean(PlayerPrefs.GetInt(IsVolumeOn, 1));
        
        if(!_isVolumeOn)
            ToggleSound();
    }

    public void ToggleSound()
    {
        _isVolumeOn = !_isVolumeOn;

        if(_isVolumeOn)
            _master.audioMixer.SetFloat(_volumeParameter.ToString(), OnVolume);
        else 
            _master.audioMixer.SetFloat(_volumeParameter.ToString(), OffVolume);
        
        OnIcon.gameObject.SetActive(_isVolumeOn);
        OffIcon.gameObject.SetActive(!_isVolumeOn);
        
        PlayerPrefs.SetInt(IsVolumeOn, _isVolumeOn ? 1 : 0);
    }
}
