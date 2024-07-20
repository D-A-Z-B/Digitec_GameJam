using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovingState : HeadState
{
    public HeadMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    private float lastAttackTime;
    private bool extraMove = false;

    private Vector2 moveDir;
    private Vector2 startPos;
    private Vector2 mousePos;
    private Coroutine moveRoutine;
    private bool first = true;

    public override void Enter()
    {
        base.Enter();
        // 공격 시점 기록

        if (extraMove == false)
        {
            if (first) {
                first = false;
            }
            else {
                if (Time.time < lastAttackTime + head.attackCoolDown) {
                    stateMachine.ChangeState(HeadStateEnum.OnBody);
                    return;
                }
                else {
                    lastAttackTime = Time.time;
                }
            }
        }

        head.ReturnPositionList.Push(head.transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDir = (mousePos - (Vector2)head.transform.position).normalized;
        startPos = head.transform.position;
        
        head.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg -90);

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
                }
                else {
                    if (JustEvasionCheck()) {
                        extraMove = true;
                        stateMachine.ChangeState(HeadStateEnum.JustMoving);
                    }
                    else {
                        stateMachine.ChangeState(HeadStateEnum.Return);
                    }
                }
                yield break;
            }
            if (CollisionCheck()) {
                stateMachine.ChangeState(HeadStateEnum.Return);
            }
            head.transform.position += (Vector3)(head.attackSpeed * Time.deltaTime * moveDir);
            yield return null;
        }
    }

    public override void Exit() {
        if (moveRoutine != null) {
            head.StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
        base.Exit();
    }

    public bool CollisionCheck() {
        Collider2D collider = Physics2D.OverlapCircle(head.transform.position, 0.5f, head.returnLayer);
        if (collider != null) {
            if (collider.gameObject.layer == head.returnLayer) {
                return true;
            }
        }
        return false;
    }

    private bool JustEvasionCheck() {
        return false;
    }
}
