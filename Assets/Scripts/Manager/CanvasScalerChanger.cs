using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerChanger : MonoBehaviour
{
    private float defaultRadio = 0.5622f;
    private CanvasScaler canvasScaler;
    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        float radio = Screen.width / (Screen.height *1.0f);
        canvasScaler.matchWidthOrHeight = radio > defaultRadio ? 0 : 1;
    }
}
