using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEarthQuakeState : EnemyState<DinoStateEnum>
{
    public DinoEarthQuakeState(Enemy enemy, EnemyStateMachine<DinoStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }
}
