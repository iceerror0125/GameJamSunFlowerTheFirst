using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(InputConstant.Interact))
        {
            Observer.Instance.Announce(new Message(MessageType.InteractPressed));
        }
        if (Input.GetKeyDown(InputConstant.Inventory))
        {
            Observer.Instance.Announce(new Message(MessageType.ShowInventory));
        }
    }
}
