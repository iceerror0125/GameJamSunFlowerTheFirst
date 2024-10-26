using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private BaseState currentState;
    private event Action<string> onChangeState;
    public StateMachine(BaseState initState, Action<string> onChangeState = null)
    {
        this.currentState = initState;
        this.onChangeState += onChangeState;
        
        ChangeState(initState);
    }
    public void Update()
    {
        this.currentState.Update();
    }
    public void ChangeState(BaseState newState)
    {
        this.currentState.Exit();
        this.currentState = newState;
        this.currentState.Enter();
        onChangeState?.Invoke(currentState.AnimName);
    }
}
