using UnityEngine;

public class ChaseState : EnemyState<EnemyStateEnum>
{
    public ChaseState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    protected Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.Instance.Player.transform;
    }

    public override void UpdateState() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;

        _enemy.FlipController(direction.normalized.x);
        if(_enemy.nearDistance < direction.magnitude) {
            Move();
        }
        
        if(_enemy.CanAttack() && _enemy.IsPlayerDetected()) {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }

    protected virtual void Move() { }
}
