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
        if (returnRoutine != null)
        {
            head.StopCoroutine(returnRoutine);
        }
        returnRoutine = head.StartCoroutine(ReturnRoutine());
    }

    public override void Exit()
    {
        head.ReturnPositionList.Clear();
        if (returnRoutine != null)
        {
            head.StopCoroutine(returnRoutine);
            returnRoutine = null;
        }
        base.Exit();
    }

    private IEnumerator ReturnRoutine() 
    {
        Vector2 moveDir;
        while (head.ReturnPositionList.Count > 0)
        {
            Vector2 startPos = head.transform.position;
            Vector2 endPos = head.ReturnPositionList.Pop();
            moveDir = (endPos - startPos).normalized;

            while (Vector2.Distance(head.transform.position, endPos) > 0.1f)
            {
                head.transform.position += (Vector3)(head.attackSpeed * 2 * Time.deltaTime * moveDir);
                yield return null;
            }

            head.transform.position = endPos; 
            head.MovementCompo.StopImmediately();
        }

        Vector2 playerPos = new Vector2(head.player.transform.position.x, head.player.transform.position.y + 0.8f);
        moveDir = (playerPos - (Vector2)head.transform.position).normalized;
        while (Vector2.Distance(head.transform.position, playerPos) > 0.1f)
        {
            playerPos = new Vector2(head.player.transform.position.x, head.player.transform.position.y + 0.8f);
            moveDir = (playerPos - (Vector2)head.transform.position).normalized;
            head.transform.position += (Vector3)(head.attackSpeed * 2 * Time.deltaTime * moveDir);
            yield return null;
        }  

        head.transform.position = playerPos;
        head.MovementCompo.StopImmediately();
        stateMachine.ChangeState(HeadStateEnum.OnBody);
    }
}
