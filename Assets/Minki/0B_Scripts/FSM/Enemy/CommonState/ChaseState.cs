using UnityEngine;

public class ChaseState : EnemyState<EnemyStateEnum>
{
    public ChaseState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    protected Transform _playerHeadTrm;

    public override void Enter() {
        base.Enter();

        _playerHeadTrm = PlayerManager.Instance.Head.transform;
    }

    public override void UpdateState() {
        Vector2 direction = _playerHeadTrm.position - _enemy.transform.position;

        _enemy.FlipController(direction.normalized.x);
        if(_enemy.nearDistance < direction.magnitude) {
            Move();
        }
        else _enemy.StopImmediately(false);
        
        if(_enemy.CanAttack() && _enemy.attackDistance > direction.magnitude) {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }

    protected virtual void Move() { }
}
