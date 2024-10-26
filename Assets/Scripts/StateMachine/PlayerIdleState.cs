using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    private bool isMove;
    
    public PlayerIdleState(Player player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        isMove = false;
        player.Idle();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.Instance.IsFreeze)
            return;
        
        isMove = IsMove();
        if (isMove)
        {
            player.stateHandler.ChangeState(player.stateHandler.moveState);
        }
    }

    private bool IsMove()
    {
        if (Input.GetKeyDown(InputConstant.MoveDown))
        {
            return true;
        }
        if (Input.GetKeyDown(InputConstant.MoveUp))
        {
            return true;
        }
        if (Input.GetKeyDown(InputConstant.MoveLeft))
        {
            return true;
            
        }
        if (Input.GetKeyDown(InputConstant.MoveRight))
        {
            return true;
        }

        return false;
    }
}
