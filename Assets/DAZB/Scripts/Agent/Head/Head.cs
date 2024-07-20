using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeadStateEnum {
    OnBody,
    Moving,
    JustMoving,
    Return
}

public class Head : Agent {
    [Header("Setting Values")]
    public float attackSpeed;
    public float attackCoolDown;
    public float neckDistance;
    public float returnSpeed;
    public LayerMask returnLayer;
    public GameObject ShockWave;
    public HeadStateMachine StateMachine {get; protected set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;
    public Player player {get; protected set;}
    [HideInInspector] public Stack<Vector2> ReturnPositionList = new Stack<Vector2>();

    protected override void Awake()
    {
        base.Awake();
        player = PlayerManager.Instance.Player;
        StateMachine = new HeadStateMachine();
        foreach (HeadStateEnum stateEnum in Enum.GetValues(typeof(HeadStateEnum))) {
            string typeName = stateEnum.ToString();
            try {
                Type t = Type.GetType($"Head{typeName}State");
                HeadState state = Activator.CreateInstance(t, this, StateMachine, typeName) as HeadState;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex) {
                Debug.LogError($"{typeName} is loading error! check Message");
                Debug.LogError(ex.Message);
            }
        }
    }

    protected void Start() {
        StateMachine.Initialize(HeadStateEnum.OnBody, this);
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }
}
