using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
       
    }

    public override void Update()
    {
        if (GameManager.Instance.IsFreeze)
        {
            player.stateHandler.ChangeState(player.stateHandler.idleState);
        }
    }

    public override void Exit()
    {
    }

}
