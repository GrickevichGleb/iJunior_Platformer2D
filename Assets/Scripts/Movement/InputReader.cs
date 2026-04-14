using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private KeyCode _jumpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _attackKey = KeyCode.Space;

    public event Action<Vector2> InputAxesChanged;

    public event Action JumpKeyPressed;
    public event Action AttackKeyPressed;

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
        if(Input.GetKeyDown(_jumpKey))
            JumpKeyPressed?.Invoke();
        
        if(Input.GetKeyDown(_attackKey))
            AttackKeyPressed?.Invoke();
    }
}
