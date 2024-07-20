using System.Collections;
using UnityEngine;

public class HeadReturnState : HeadState
{
    public HeadReturnState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    private Coroutine returnRoutine;

    public override void Enter()
    {
        base.Enter();
        if (head.AbilityReignite) {
            head.StartDelayCallback(0.3f, () => stateMachine.ChangeState(HeadStateEnum.OnBody));
        }
        if (returnRoutine != null)
        {
            head.StopCoroutine(returnRoutine);
        }
        head.StartDelayCallback(0.3f, () => returnRoutine = head.StartCoroutine(ReturnRoutine()));
    }

    public override void Exit()
    {
        head.ReturnPositionList.Clear();
        if (returnRoutine != null)
        {
            head.StopCoroutine(returnRoutine);
            returnRoutine = null;
        }
        head.transform.eulerAngles = Vector2.zero;
        base.Exit();
    }

    private IEnumerator ReturnRoutine() {
        while (head.ReturnPositionList.Count > 0) {
            Vector2 startPos = head.transform.position;
            Vector2 endPos = head.ReturnPositionList.Pop();
            while (Vector2.Distance(head.transform.position, endPos) > 0.1f)
            {
                Vector2 currentPos = head.transform.position;
                Vector2 moveDir = (endPos - currentPos).normalized;
                head.transform.position += (Vector3)(head.attackSpeed * 2 * Time.deltaTime * moveDir);
                yield return null;
            }
            head.MovementCompo.StopImmediately();
        }
        Vector2 playerPos = new Vector2(head.player.transform.position.x, head.player.transform.position.y + head.neckDistance);
        while (Vector2.Distance(head.transform.position, playerPos) > 0.1f) {
            Vector2 currentPos = head.transform.position;
            Vector2 moveDir = (playerPos - currentPos).normalized;
            head.transform.position += (Vector3)(head.returnSpeed * Time.deltaTime * moveDir);
            yield return null;
        }
        head.MovementCompo.StopImmediately();
        stateMachine.ChangeState(HeadStateEnum.OnBody);
    }
}
