using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngles = Vector3.zero;
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(rotationAngles);
    }
}
