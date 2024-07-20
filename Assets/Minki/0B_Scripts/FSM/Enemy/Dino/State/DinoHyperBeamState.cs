using System.Collections;
using UnityEngine;

public class DinoHyperBeamState : EnemyState<DinoStateEnum>
{
    public DinoHyperBeamState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private int _count = 0;
    private Transform _playerHeadTrm;

    private Dino _dino;

    public override void Enter() {
        base.Enter();

        _playerHeadTrm = PlayerManager.Instance.Head.transform;

        _dino = _enemy as Dino;

        _enemy.StartCoroutine(FollowCoroutine(3));
    }

    public override void UpdateState() {
        if(_endTriggerCalled) {
            if(++_count == 1) _enemy.StartCoroutine(FollowCoroutine(3));
            else if(++_count == 2) _enemy.StartCoroutine(FollowCoroutine(6));
            else _stateMachine.ChangeState(DinoStateEnum.Chase);
        }
    }

    private IEnumerator FollowCoroutine(float height) {
        float timer = 0f;

        while(timer < 2f) {
            timer += Time.deltaTime;

            _enemy.transform.position = new Vector2(_enemy.transform.position.x, _playerHeadTrm.position.y - 5f);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        _dino.warning.ShowRange(new Vector2(0, _enemy.transform.position.y - height / 2f), new Vector3(30f, height), 0.5f);

        yield return new WaitForSeconds(0.5f);

        _dino.Laser(height);
    }
}
