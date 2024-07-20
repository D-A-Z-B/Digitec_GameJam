using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateEnum {
    Idle,
    Walk,
    Jump,
    Fall,
}

public class Player : Agent
{
    [Header("Setting Values")]
    public float moveSpeed;
    public float limitJumpPower;
    public int attackDamage;
    public int returnDamage;
    public PlayerStateMachine StateMachine {get; protected set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;

    protected override void Awake() {
        returnDamage = attackDamage / 2;
        base.Awake();
        StateMachine = new PlayerStateMachine();
        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum))) {
            string typeName = stateEnum.ToString();
            try {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex) {
                Debug.LogError($"{typeName} is loading error! check Message");
                Debug.LogError(ex.Message);
            }
        }
    }

    protected void Start() {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }
}
