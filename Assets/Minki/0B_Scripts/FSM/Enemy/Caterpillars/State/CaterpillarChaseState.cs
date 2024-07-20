using UnityEngine;

public class CaterpillarChaseState : ChaseState
{
    public CaterpillarChaseState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Caterpillar _caterpillar;

    public override void Enter() {
        base.Enter();

        _caterpillar = _enemy as Caterpillar;
    }

    public override void UpdateState() {
        Vector2 direction = _playerHeadTrm.position - _enemy.transform.position;
        float distance = Mathf.Abs(_playerHeadTrm.position.x - _enemy.transform.position.x);

        _enemy.FlipController(direction.normalized.x);
        if(_enemy.nearDistance < distance) {
            Move();
        }
        else _enemy.StopImmediately(false);
        
        if(_enemy.CanAttack() && _enemy.attackDistance >= distance) {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }

        if(_caterpillar.moveTriggerCalled) {
            Move();
        }
    }

    protected override void Move() {
        Vector2 direction = _playerHeadTrm.position - _enemy.transform.position;
        _enemy.FlipController(direction.normalized.x);

        _enemy.SetVelocity(_enemy.FacingDirection * _enemy.moveSpeed, 0);
        _enemy.StartDelayCallback(0.8f, () => _enemy.StopImmediately(false));
    }
}
