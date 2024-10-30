using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    private bool isFreeze;
    public bool IsFreeze => isFreeze;
    public int part = 1;

    [ContextMenu("Test")]
    public void ChangePart()
    {
        part = 2;
        Observer.Instance.Announce(new Message(MessageType.ChangeToPart2));
    }
    public void Freeze()
    {
        isFreeze = true;
    }

    public void UnFreeze()
    {
        isFreeze = false;
    }
    
}
