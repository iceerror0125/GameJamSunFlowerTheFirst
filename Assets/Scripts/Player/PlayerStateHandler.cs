using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateHandler
{
    private Player player;
    private Animator animator;
    #region State
    private StateMachine stateMachine;
    public PlayerIdleState idleState {get; private set;}
    public PlayerMoveState moveState {get; private set;}
    #endregion
    

    public void Init(Player player, Animator animator)
    {
        SetDefaultParams(player, animator);
        InitState();
    }

    private void SetDefaultParams(Player player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

    private void InitState()
    {
        idleState = new PlayerIdleState(player, "idle");
        moveState = new PlayerMoveState(player, "move");
        
        stateMachine = new StateMachine(idleState, ChangeAnim);
    }

    public void ChangeState(PlayerState newState)
    {
        stateMachine.ChangeState(newState);
    }

    private void ChangeAnim(string animName)
    {
        this.animator.Play(animName);
    }

    public void Update()
    {
        stateMachine.Update();
    }

}
