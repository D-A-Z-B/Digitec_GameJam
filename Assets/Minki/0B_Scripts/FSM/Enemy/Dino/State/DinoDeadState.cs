using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoDeadState : EnemyState<DinoStateEnum>
{
    public DinoDeadState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }


}
