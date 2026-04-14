using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChanger : MonoBehaviour
{
    protected Button Button;
    
    private void Start()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }

    protected virtual void OnButtonClick() { }
}
