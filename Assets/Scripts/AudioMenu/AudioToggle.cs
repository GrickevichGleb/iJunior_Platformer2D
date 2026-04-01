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

    [SerializeField] private Button _button;
    
    [SerializeField] private Image _onIcon;
    [SerializeField] private Image _offIcon;

    [SerializeField] private AudioMixerGroup _master;
    [SerializeField] private VolumeParameters _volumeParameter;
    
    private bool _isVolumeOn = true;

    private void Start()
    {
        _isVolumeOn = Convert.ToBoolean(PlayerPrefs.GetInt(IsVolumeOn, 1));
        
        if(!_isVolumeOn)
            ToggleSound();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ToggleSound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ToggleSound);
    }

    public void ToggleSound()
    {
        _isVolumeOn = !_isVolumeOn;

        if(_isVolumeOn)
            _master.audioMixer.SetFloat(_volumeParameter.ToString(), OnVolume);
        else 
            _master.audioMixer.SetFloat(_volumeParameter.ToString(), OffVolume);
        
        _onIcon.gameObject.SetActive(_isVolumeOn);
        _offIcon.gameObject.SetActive(!_isVolumeOn);
        
        PlayerPrefs.SetInt(IsVolumeOn, _isVolumeOn ? 1 : 0);
    }
}
