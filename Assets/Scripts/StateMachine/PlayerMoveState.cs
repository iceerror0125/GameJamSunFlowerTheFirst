using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerMoveState : PlayerGroundState
{
    private MoveDirection moveDirection;
    private KeyCode currentKeycode;

    public PlayerMoveState(Player player, string animName) : base(player, animName)
    {
    }

    public void SetDirection(MoveDirection moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public override void Enter()
    {
        Update();
    }

    public override void Update()
    {
        base.Update();
        
        CheckMovingState(InputConstant.MoveLeft, MoveDirection.Left);
        CheckMovingState(InputConstant.MoveRight, MoveDirection.Right);
        CheckMovingState(InputConstant.MoveUp, MoveDirection.Up);
        CheckMovingState(InputConstant.MoveDown, MoveDirection.Down);
        
    }
    

    public override void Exit()
    {
    }

    private void CheckMovingState(KeyCode key, MoveDirection direction)
    {
        CheckMoving(key, direction);
        CheckStopping(key);
    }

    private void CheckStopping(KeyCode key)
    {
        if (Input.GetKeyUp(key))
        {
            if (key == currentKeycode)
            {
                player.stateHandler.ChangeState(player.stateHandler.idleState);
            }
        }
    }

    private void CheckMoving(KeyCode key, MoveDirection direction)
    {
        if (Input.GetKey(key))
        {
            player.Move(direction);
            currentKeycode = key;
        }
    }
}