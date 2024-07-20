using UnityEngine;

public class DinoChaseState : EnemyState<DinoStateEnum>
{
    public DinoChaseState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    
}
