using System;
using UnityEngine;

public class Caterpillar : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;

    [HideInInspector] public bool moveTriggerCalled = false;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
        
        foreach(EnemyStateEnum stateEnum in Enum.GetValues(typeof(EnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Caterpillar{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Caterpillar] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(EnemyStateEnum.Chase, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }
    public override void Attack() {
        Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }
}
