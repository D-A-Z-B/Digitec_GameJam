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
        if (extraMove == false)
        {
            if (Time.time > lastAttackTime + head.attackCoolDown)
            {
                stateMachine.ChangeState(HeadStateEnum.OnBody);
            }
        }

        head.ReturnPositionList.Push(head.transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDir = (mousePos - (Vector2)head.transform.position).normalized;
        startPos = head.transform.position;

        if (moveRoutine != null)
        {
            head.StopCoroutine(moveRoutine);
        }
        moveRoutine = head.StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        float elapsedTime = 0f;
        float duration = 1f / head.attackSpeed;
        float distance = Vector2.Distance(startPos, mousePos);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = EaseOutQuart(t);
            head.transform.position = Vector2.Lerp(startPos, mousePos, easedT);
            moveDir = (mousePos - (Vector2)head.transform.position).normalized;
            Debug.Log(moveDir);
            head.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90);
            elapsedTime += Time.deltaTime;

            if (Vector2.Distance(head.transform.position, mousePos) <= 0.1f)
            {
                head.MovementCompo.StopImmediately();
                if (extraMove)
                {
                    extraMove = false;
                    stateMachine.ChangeState(HeadStateEnum.Return);
                }
                else
                {
                    if (JustEvasionCheck())
                    {
                        extraMove = true;
                        stateMachine.ChangeState(HeadStateEnum.JustMoving);
                    }
                    else
                    {
                        stateMachine.ChangeState(HeadStateEnum.Return);
                    }
                }
                yield break;
            }

            if (Vector2.Distance(startPos, head.transform.position) > head.attackRange) {
                stateMachine.ChangeState(HeadStateEnum.Return);
                yield break;
            }

            if (CollisionCheck())
            {
                stateMachine.ChangeState(HeadStateEnum.Return);
                yield break;
            }

            yield return null;
        }

        // 종료 시점 처리
        head.transform.position = mousePos;
        head.MovementCompo.StopImmediately();
        stateMachine.ChangeState(HeadStateEnum.Return);
    }

    private float EaseOutQuart(float t)
    {
        return 1 - Mathf.Pow(1 - t, 4);
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        if (moveRoutine != null)
        {
            head.StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }

    private Collider2D[] collider2DResults = new Collider2D[10];
    public bool CollisionCheck()
    {
        int numColliders = Physics2D.OverlapCircleNonAlloc(head.transform.position, 0.5f, collider2DResults, head.returnLayer);
        if (numColliders > 0) {
            return true;
        }
        return false;
    }

    private bool JustEvasionCheck() {
        return false;
    }
}