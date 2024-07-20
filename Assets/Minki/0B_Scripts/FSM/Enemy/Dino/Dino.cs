using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DinoStateEnum {
    Chase, EarthQuake, Spit, HyperBeam, Groggy, Dead
}

public class Dino : Enemy
{
    public new EnemyStateMachine<DinoStateEnum> StateMachine { get; protected set; }
    [HideInInspector] public int usingPattern;
    
    public Warning warning;
    
    [SerializeField] private CaterpillarProjectile _meteor;

    [SerializeField] private Transform _enemySpawnPosition;
    [SerializeField] private Enemy[] _enemies;

    [SerializeField] private DinoLaser _laser;

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

    public void Meteor() {
        for(int i = 0; i < 3; ++i) {
            float x = Random.Range(-12f, 12f);
            StartCoroutine(MeteorCoroutine(x));
        }
    }

    private IEnumerator MeteorCoroutine(float x) {
        warning.ShowRange(new Vector3(x + 23f, 0), new Vector2(2, 20), 1f);

        yield return new WaitForSeconds(1f);

        Instantiate(_meteor, transform.position + new Vector3(x, 20), Quaternion.identity);
    }

    public override void Attack() {
        if(StateMachine.CurrentState is DinoSpitState) {
            Instantiate(_enemies[Random.Range(0, 2)], _enemySpawnPosition.position, Quaternion.identity);
        }
    }

    public void Laser(float height) {
        GameObject obj = Instantiate(_laser, transform.position, Quaternion.identity).gameObject;
        obj.transform.localScale = new Vector3(20f, height, 1f);
        Destroy(obj, 2f);
    }
}
