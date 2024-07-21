using System;
using UnityEngine;

public class Airplane : Enemy
{
    [HideInInspector] public bool successAttack = false;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
        
        foreach(EnemyStateEnum stateEnum in Enum.GetValues(typeof(EnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Airplane{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Airplane] : Not Found State [{typeName}]");
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
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!successAttack && (_whatIsPlayer.value & (1 << other.gameObject.layer)) > 0) {
            if(other.gameObject.TryGetComponent(out IDamageable health)) {
                health.ApplyDamage(1, transform);
                successAttack = true;
                Debug.Log($"Success Attack! | {StateMachine.CurrentState}");
            }
        }
    }
}
