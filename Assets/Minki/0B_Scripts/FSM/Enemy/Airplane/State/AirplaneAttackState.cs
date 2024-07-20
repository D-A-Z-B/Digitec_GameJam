using UnityEngine;

public class AirplaneAttackState : AttackState
{
    public AirplaneAttackState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerHeadTrm;

    private Airplane _airplane;

    public override void Enter() {
        base.Enter();

        _playerHeadTrm = PlayerManager.Instance.Head.transform;

        _airplane = _enemy as Airplane;
    }

    public override void UpdateState() {
        Move();

        if(_airplane.successAttack) {
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }
    }

    public override void Exit() {
        _airplane.successAttack = false;

        base.Exit();
    }

    private void Move() {
        Vector2 direction = (_playerHeadTrm.position - _enemy.transform.position).normalized;

        _enemy.FlipController(direction.normalized.x);
        _enemy.SetVelocity(direction.x * (_enemy.moveSpeed + 4), direction.y * _enemy.moveSpeed * 1.5f);
    }
}
