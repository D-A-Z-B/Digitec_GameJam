using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovingState : HeadState
{
    public HeadMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    float lastAttackTime;
    bool extraMove = false;

    Vector2 moveDir;
    Vector2 startPos;
    Vector2 mousePos;
    Coroutine moveRoutine;

    public override void Enter()
    {
        base.Enter();
        if (extraMove == false) {
            if (Time.time > lastAttackTime + head.attackCoolDown) {
                stateMachine.ChangeState(HeadStateEnum.OnBody);
                return;
            }
        }

        head.ReturnPositionList.Push(head.transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDir = (mousePos - (Vector2)head.transform.position).normalized;
        startPos = head.transform.position;

        if (moveRoutine != null) {
            head.StopCoroutine(moveRoutine);
        }
        moveRoutine = head.StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine() {
        while (true) {
            if (Vector2.Distance(head.transform.position, mousePos) <= 0.1f) {
                head.MovementCompo.StopImmediately();
                if (extraMove == true) {
                    extraMove = false;
                    stateMachine.ChangeState(HeadStateEnum.Return);
                } else {
                    if (JustEvasionCheck()) {
                        extraMove = true;
                        stateMachine.ChangeState(HeadStateEnum.JustMoving);
                    } else {
                        stateMachine.ChangeState(HeadStateEnum.Return);
                    }
                }
                yield break;
            }
            head.transform.position += (Vector3)(head.attackSpeed * Time.deltaTime * moveDir);
            yield return null;
        }
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        if (moveRoutine != null) {
            head.StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }

    private bool JustEvasionCheck() {
        return true;
    }
}
