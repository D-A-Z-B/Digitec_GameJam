using UnityEngine;

public enum EnemyStateEnum {
    Chase, Attack, Dead
}

public abstract class Enemy : Agent {
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; private set; }

    [HideInInspector] public DamageCaster DamageCasterCompo;

    [Header("Movement Settings")]
    public LayerMask whatIsPlatform;
    public float jumpPower = 11f;

    [Header("Check Settings")]
    public float nearDistance;
    [SerializeField] private  LayerMask _whatIsPlayer;
    public LayerMask whatIsObstacle;

    [Header("Attack Settings")]
    public Vector2 attackRange;
    public Vector2 attackOffset;
    public float attackCooldown;
    [HideInInspector] public float lastAttackTime;

    protected int _lastAnimationBoolHash;

    protected override void Awake() {
        base.Awake();

        DamageCasterCompo = GetComponent<DamageCaster>();

        HealthCompo = GetComponent<Health>();
        HealthCompo.SetOwner(this);
        HealthCompo.OnDead += HandleDead;
    }

    private void Start() {
        StateMachine.Initialize(EnemyStateEnum.Chase, this);
    }

    public virtual void AssignLastAnimationHash(int hashCode) {
        _lastAnimationBoolHash = hashCode;
    }

    public virtual int GetLastAnimationHash() => _lastAnimationBoolHash;

    public abstract void AnimationFinishTrigger();

    public bool CanAttack() {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public virtual bool IsPlayerDetected() {
        return Physics2D.OverlapBox((Vector2)transform.position * FacingDirection, attackRange, 0, _whatIsPlayer);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + attackOffset * FacingDirection, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, nearDistance);
    }

    #endif
}
