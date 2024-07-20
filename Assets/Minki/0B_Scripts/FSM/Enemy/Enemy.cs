using UnityEngine;

public enum EnemyStateEnum {
    Chase, Attack, Dead
}

public abstract class Enemy : Agent {
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; protected set; }

    [HideInInspector] public DamageCaster DamageCasterCompo;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Check Settings")]
    public float nearDistance;
    [SerializeField] private  LayerMask _whatIsPlayer;
    public LayerMask whatIsObstacle;

    [Header("Attack Settings")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastAttackTime;

    protected int _lastAnimationBoolHash;

    protected override void Awake() {
        base.Awake();

        Transform visualTrm = transform.Find("Visual");
        SpriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
        AnimatorCompo = visualTrm.GetComponent<Animator>();

        DamageCasterCompo = GetComponent<DamageCaster>();

        HealthCompo = GetComponent<Health>();
        HealthCompo.SetOwner(this);
        HealthCompo.OnDead += HandleDead;
    }

    public virtual void AssignLastAnimationHash(int hashCode) {
        _lastAnimationBoolHash = hashCode;
    }

    public virtual int GetLastAnimationHash() => _lastAnimationBoolHash;

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public bool CanAttack() {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public virtual bool IsObstacleInLine(float distance, Vector3 direction) {
        return Physics2D.Raycast(transform.position, direction, distance, whatIsObstacle);
    }

    public abstract void Attack();

    protected virtual void HandleDead() {
        StateMachine.ChangeState(EnemyStateEnum.Dead);
    }

    #if UNITY_EDITOR

    protected virtual void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, nearDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    #endif
}
