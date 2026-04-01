using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonsAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;

    private Dictionary<Button, int> _buttonsClips = new Dictionary<Button, int>();

    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void RegisterButton(Button button, int index)
    {
        if (_buttonsClips.ContainsKey(button))
            return;
        
        _buttonsClips.Add(button, index);
    }

    public void PlayButtonClip(Button button)
    {
        if (!_buttonsClips.ContainsKey(button))
            return;
        
        if(_buttonsClips[button] < 0 || _buttonsClips[button] >= _audioClips.Length)
            return;
        
        _audioSource.Stop();
        _audioSource.PlayOneShot(_audioClips[_buttonsClips[button]]);
    }
}
