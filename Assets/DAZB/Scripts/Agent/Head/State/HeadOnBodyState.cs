using System;
using UnityEngine;

public class HeadOnBodyState : HeadState
{
    public HeadOnBodyState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        head.InputReader.HeadMoveEvent += HeadMoveEventHandle;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        head.transform.position = head.player.transform.position + new Vector3(0, head.neckDistance);
    }

    private void HeadMoveEventHandle()
    {
        stateMachine.ChangeState(HeadStateEnum.Moving);
    }

    public override void Exit() {
        base.Exit();
        head.InputReader.HeadMoveEvent -= HeadMoveEventHandle;
    }
}