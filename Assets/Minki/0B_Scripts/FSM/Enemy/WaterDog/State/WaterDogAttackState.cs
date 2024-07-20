using UnityEngine;

public class WaterDogAttackState : AttackState
{
    public WaterDogAttackState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerHeadTrm;

    public override void Enter() {
        base.Enter();

        _playerHeadTrm = PlayerManager.Instance.Head.transform;

        Vector2 direction = _playerHeadTrm.position - _enemy.transform.position;
        direction += Vector2.up * 5f;

        direction.Normalize();

        _enemy.RigidbodyCompo.AddForce(direction * 7.5f, ForceMode2D.Impulse);
        Debug.Log(direction);
    }
}
