using System;
using UnityEngine;

public class WaterDog : Enemy
{
    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
        
        foreach(EnemyStateEnum stateEnum in Enum.GetValues(typeof(EnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"WaterDog{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Water Dog] : Not Found State [{typeName}]");
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

    private void OnCollisionEnter2D(Collision2D other) {
        if((_whatIsPlayer.value & (1 << other.gameObject.layer)) > 0) {
            if(other.transform.TryGetComponent(out IDamageable health)) {
                health.ApplyDamage(1, transform);
            }
        }
    }
}
