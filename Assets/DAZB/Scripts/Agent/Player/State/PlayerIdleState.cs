using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (player.InputReader.Movement.sqrMagnitude > 0.01f) {
            stateMachine.ChangeState(PlayerStateEnum.Walk);
        }
    }
}
