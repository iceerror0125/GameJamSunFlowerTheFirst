using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    private bool isFreeze;
    public bool IsFreeze => isFreeze;

    public void Freeze()
    {
        isFreeze = true;
    }

    public void UnFreeze()
    {
        isFreeze = false;
    }
    
}
