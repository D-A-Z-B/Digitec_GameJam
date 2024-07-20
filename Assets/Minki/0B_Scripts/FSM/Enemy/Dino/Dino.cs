using System;
using UnityEngine;

public enum DinoStateEnum {
    Chase, EarthQuake, Spit, HyperBeam, Groggy, Dead
}

public class Dino : Enemy
{
    public new EnemyStateMachine<DinoStateEnum> StateMachine { get; protected set; }
    [HideInInspector] public int usingPattern;

    [SerializeField] private Enemy[] _enemies;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<DinoStateEnum>();
        
        foreach(DinoStateEnum stateEnum in Enum.GetValues(typeof(DinoStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Dino{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<DinoStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Dino] : Not Found State [{typeName}]");
            }
        }

        HealthCompo.OnHit += CheckGroggy;
    }

    private void Start() {
        StateMachine.Initialize(DinoStateEnum.Chase, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    private void CheckGroggy() {
        if(usingPattern >= 4) {
            usingPattern = 0;
            StateMachine.ChangeState(DinoStateEnum.Groggy, true);
        }
    }

    protected override void HandleDead() {
        StateMachine.ChangeState(DinoStateEnum.Dead);
    }

    public override void Attack() {
        
    }
}
