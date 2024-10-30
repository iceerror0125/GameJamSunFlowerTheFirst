using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickNumberItem : MonoBehaviour
{
    [SerializeField] private TMP_Text textValue;
    private int value;
    public string Value => value.ToString();

    private void Start()
    {
        SetTextValue();
    }

    public void IncreaseValue()
    {
        value++;
        if (value > 9)
        {
            value = 0;
        }
        SetTextValue();
    }

    public void DecreaseValue()
    {
        value--;
        if (value < 0)
        {
            value = 9;
        }
        SetTextValue();
    }

    private void SetTextValue()
    {
        textValue.text = value.ToString();
    }

}
