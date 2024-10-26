using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected string animName;
    public string AnimName => animName;
    protected BaseState(string animName)
    {
        this.animName = animName;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
