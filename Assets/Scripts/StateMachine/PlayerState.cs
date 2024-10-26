using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : BaseState
{
    protected Player player;

    public PlayerState(Player player, string animName) : base(animName)
    {
        this.player = player;
        this.animName = animName;
    }
    public abstract override void Enter();
    public abstract override void Update();
    public abstract override void Exit();
}

