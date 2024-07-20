using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadJustMovingState : HeadState
{
    public HeadJustMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        head.StartCoroutine(JustMovingRoutine());
    }

    private IEnumerator JustMovingRoutine() {
        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);
        stateMachine.ChangeState(HeadStateEnum.Moving);
    }
}