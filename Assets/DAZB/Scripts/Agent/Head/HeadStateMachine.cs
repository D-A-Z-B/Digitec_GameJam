using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStateMachine
{
    public HeadState CurrentState {get; private set;}
    public Dictionary<HeadStateEnum, HeadState> stateDictionary;
    public HeadStateEnum CurrentStateEnum;
    private Head head;

    public HeadStateMachine() {
        stateDictionary = new Dictionary<HeadStateEnum, HeadState>();
    }

    public void Initialize(HeadStateEnum startState, Head head) {
        this.head = head;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }
    public void ChangeState(HeadStateEnum newState) {
        if (head.CanStateChangeable == false) return;
        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        Debug.Log(newState);
        CurrentStateEnum = newState;
        CurrentState.Enter();
    }

    public void AddState(HeadStateEnum headStateEnum, HeadState headState) {
        stateDictionary.Add(headStateEnum, headState);
    }
}
