using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarIndicatorSmooth : HealthIndicator
{
    private const float Tolerance = 0.0001f;
    
    [SerializeField] private Image _fillerImage;
    [SerializeField] private float _changeSpeed = 1f;
    
    private Coroutine _displayChangesCoroutine;
    
    private void Start()
    {
        _displayChangesCoroutine = StartCoroutine(ChangeFillAmountCoroutine());
    }

    protected override void OnHealthChanged()
    {
        if(_displayChangesCoroutine != null)
            StopCoroutine(_displayChangesCoroutine);

        _displayChangesCoroutine = StartCoroutine(ChangeFillAmountCoroutine());
    }

    private IEnumerator ChangeFillAmountCoroutine()
    {
        float targetAmount = Convert.ToSingle(Health.Current) / Convert.ToSingle(Health.Max);

        while (Math.Abs(_fillerImage.fillAmount - targetAmount) > Tolerance)
        {
            _fillerImage.fillAmount = 
                Mathf.MoveTowards(_fillerImage.fillAmount, targetAmount, _changeSpeed * Time.deltaTime);
            
            yield return null;
        }
    }
}
