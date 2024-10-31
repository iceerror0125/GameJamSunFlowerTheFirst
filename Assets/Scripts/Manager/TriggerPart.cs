using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPart : MonoBehaviour
{
    bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTriggered)
                return;
            isTriggered = true;
            AudioManager.Instance.Thunder();
            GameManager.Instance.ChangePart();
        }
    }
}
