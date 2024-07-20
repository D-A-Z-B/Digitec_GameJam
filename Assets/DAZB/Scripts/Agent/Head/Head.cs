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
    public float attackRange;
    public float neckDistance;
    public float returnSpeed;
    public float JustEvasionCheckRange;
    public LayerMask returnLayer;
    public GameObject ShockWave;
    public HeadStateMachine StateMachine {get; protected set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;
    public Player player {get; protected set;}
    [HideInInspector] public Stack<Vector2> ReturnPositionList = new Stack<Vector2>();
    public bool AbilityReignite = false;
    public bool AbilityComeBack = false;
    public bool AbilityGravitation = false;
    public bool AbilitySpark = false;
    public bool AbilityApShot = false;
    public Action SparkEvent;
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

    private Collider2D[] result = new Collider2D[10];
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

            // Draw the collision check circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        // Draw the evasion check circle
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f);

        // Draw the raycast for the closest enemy
        int numEnemies = Physics2D.OverlapCircleNonAlloc(transform.position, 5, result, LayerMask.GetMask("Enemy"));
        if (numEnemies > 0) {
            Collider2D closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < numEnemies; i++) {
                float distance = Vector2.Distance(transform.position, result[i].transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestEnemy = result[i];
                }
            }

            if (closestEnemy != null) {
                Vector2 direction = (transform.position - closestEnemy.transform.position).normalized;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(closestEnemy.transform.position, closestEnemy.transform.position + (Vector3)(direction * JustEvasionCheckRange));
            }
        }
    }
}
