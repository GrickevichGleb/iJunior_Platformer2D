using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private KeyCode[] _actionKeys;
   
    public event Action<Vector2> InputAxesChanged;
    public event Action<KeyCode> KeyPressed;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        ReadAxes();
        ReadKeys();
    }

    private void ReadAxes()
    {
        Vector2 newInput = new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));
        InputAxesChanged?.Invoke(newInput);
    }

    private void ReadKeys()
    {
        foreach (var key in _actionKeys)
        {
            if(Input.GetKeyDown(key))
                KeyPressed?.Invoke(key);
        }
    }
}
