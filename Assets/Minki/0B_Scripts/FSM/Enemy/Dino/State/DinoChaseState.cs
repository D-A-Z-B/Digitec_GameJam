using UnityEngine;

public class DinoChaseState : EnemyState<DinoStateEnum>
{
    public DinoChaseState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void UpdateState() {
        if(_enemy.CanAttack()) {
            // switch(Random.Range(2, 3)) {
            //     case 0: _stateMachine.ChangeState(DinoStateEnum.EarthQuake); break;
            //     case 1: _stateMachine.ChangeState(DinoStateEnum.Spit); break;
            //     case 2: _stateMachine.ChangeState(DinoStateEnum.HyperBeam); break;
            // }
            _stateMachine.ChangeState(DinoStateEnum.Spit);
        }
    }
}
