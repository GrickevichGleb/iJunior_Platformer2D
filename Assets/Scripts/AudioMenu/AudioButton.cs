using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    [SerializeField] private ButtonsAudioPlayer _buttonsAudioPlayer;
    [SerializeField] private int _index = 0;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Play);
    }

    private void Start()
    {
        _buttonsAudioPlayer.RegisterButton(_button, _index);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Play);
    }

    private void Play()
    {
        _buttonsAudioPlayer.PlayButtonClip(_button);
    }
}
