using UnityEngine;

public class AirplaneChaseState : ChaseState
{
    public AirplaneChaseState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    protected override void Move() {
        if(!_enemy.CanAttack()) {
            _enemy.StopImmediately(true);
            return;
        }

        Vector2 direction = _playerHeadTrm.position - _enemy.transform.position;
        Vector2 velocity = direction.normalized * _enemy.moveSpeed;

        _enemy.SetVelocity(velocity.x, velocity.y);

        float angle = Mathf.Atan(direction.normalized.y / direction.normalized.x) * Mathf.Rad2Deg;
        _enemy.transform.rotation = Quaternion.Euler(0, _enemy.transform.eulerAngles.y, angle);

        if(_enemy.CanAttack() && _enemy.attackDistance >= direction.magnitude) {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }
}
