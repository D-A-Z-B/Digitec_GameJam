using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSpitState : EnemyState<DinoStateEnum>
{
    public DinoSpitState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }
    

}
