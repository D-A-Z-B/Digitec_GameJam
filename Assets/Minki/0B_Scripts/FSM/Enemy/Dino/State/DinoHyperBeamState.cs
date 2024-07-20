using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoHyperBeamState : EnemyState<DinoStateEnum>
{
    public DinoHyperBeamState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }
    

}
