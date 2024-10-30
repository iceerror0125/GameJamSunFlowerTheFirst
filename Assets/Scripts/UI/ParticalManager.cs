using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoBehaviour
{
    [SerializeField] private GameObject particalObj;
    [SerializeField] private float hideCountdown = 1;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(MessageType.ShowPartical, Show);
    }

    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(MessageType.ShowPartical, Show);

    }

    private void Show(Message msg)
    {
        particalObj.SetActive(true);
        Invoke(nameof(Hide), hideCountdown);
    }

    private void Hide()
    {
        particalObj.SetActive(false);
    }
}
