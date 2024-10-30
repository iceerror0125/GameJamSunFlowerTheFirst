using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RiddeType
{
    None,
    FillHole,
    CheckPassword
}

public class RiddeItem : BaseItem
{
    public override void Interact()
    {
        base.Interact();
        Observer.Instance.Announce(new Message(MessageType.ShowRiddleItem, id));
    }
}
