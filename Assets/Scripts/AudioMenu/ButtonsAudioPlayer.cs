using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonsAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _buttonsAudioClips;
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int buttonN)
    {
        if (buttonN <= 0 || buttonN > _buttonsAudioClips.Length)
            return;
        
        _audioSource.Stop();
        
        _audioSource.PlayOneShot(_buttonsAudioClips[buttonN - 1]);
    }
}
