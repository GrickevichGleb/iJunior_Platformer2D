using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarIndicator : MonoBehaviour
{
    private const float Tolerance = 0.0001f;
    
    [SerializeField] private ProgressBarValue _barValue;
    
    [SerializeField] private Image _fillerImage;
    [SerializeField] private float _changeSpeed = 1f;
    
    private Coroutine _displayChangesCoroutine;
    
    private void Start()
    {
        _displayChangesCoroutine = StartCoroutine(ChangeFillAmountCoroutine());
    }
    
    private void OnEnable()
    {
        _barValue.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _barValue.Changed -= OnChanged;
    }

    private void OnChanged()
    {
        if(_displayChangesCoroutine != null)
            StopCoroutine(_displayChangesCoroutine);

        _displayChangesCoroutine = StartCoroutine(ChangeFillAmountCoroutine());
    }
    
    private IEnumerator ChangeFillAmountCoroutine()
    {
        float targetAmount = (_barValue.Current + Mathf.Abs(_barValue.Min)) / (_barValue.Min + _barValue.Max);
        Debug.Log($"Target amount: {targetAmount}");
        
        while (Math.Abs(_fillerImage.fillAmount - targetAmount) > Tolerance)
        {
            _fillerImage.fillAmount = 
                Mathf.MoveTowards(_fillerImage.fillAmount, targetAmount, _changeSpeed * Time.deltaTime);
            
            yield return null;
        }
    }
}
