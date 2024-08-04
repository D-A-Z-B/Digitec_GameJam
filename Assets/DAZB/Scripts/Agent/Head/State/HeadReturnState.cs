using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadReturnState : HeadState
{
    public HeadReturnState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    private Coroutine returnRoutine;

    public override void Enter()
    {
        base.Enter();
        head.HealthCompo.isInvincible = true;
        if (returnRoutine != null)
        {
            head.StopCoroutine(returnRoutine);
        }

        if (head.AbilityComeBack && head.AbilityReignite) {
            head.StartDelayCallback(() => Mouse.current.leftButton.wasPressedThisFrame, () => stateMachine.ChangeState(HeadStateEnum.OnBody));
        }
        else if (head.AbilityReignite) {
            head.StartDelayCallback(0.3f, () => stateMachine.ChangeState(HeadStateEnum.OnBody));
        }
        else if (head.AbilityComeBack) {
             head.StartDelayCallback(() => Mouse.current.leftButton.wasPressedThisFrame, () => returnRoutine = head.StartCoroutine(ReturnRoutine()));
        }
        else {
            head.StartDelayCallback(0.3f, () => returnRoutine = head.StartCoroutine(ReturnRoutine()));
        }
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

    private IEnumerator ReturnRoutine()
    {
        float elapsedTime;
        float duration;
        while (head.ReturnPositionList.Count > 1)
        {
            Vector2 endPos = head.ReturnPositionList.Peek();
            Vector2 startPos = head.transform.position;
            elapsedTime = 0;
            duration = 1f / head.attackSpeed;

            while (Vector2.Distance(head.transform.position, endPos) > 0.1f)
            {
                AttackCheck();
                elapsedTime += Time.deltaTime;
                head.transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
                yield return null;
            }

            head.ReturnPositionList.Pop();
            head.MovementCompo.StopImmediately();
        }

        Vector2 playerPos = new Vector2(head.player.transform.position.x, head.player.transform.position.y + head.neckDistance);
        Vector2 startReturnPos = head.transform.position;
        elapsedTime = 0;
        duration = 1f / head.attackSpeed;

        while (Vector2.Distance(head.transform.position, playerPos) > 0.1f)
        {
            AttackCheck();
            playerPos = new Vector2(head.player.transform.position.x, head.player.transform.position.y + head.neckDistance);
            elapsedTime += Time.deltaTime * head.returnSpeed; // Speed up the interpolation
            head.transform.position = Vector2.Lerp(startReturnPos, playerPos, elapsedTime / duration);
            yield return null;
        }

        head.ReturnPositionList.Clear();
        head.MovementCompo.StopImmediately();
        stateMachine.ChangeState(HeadStateEnum.OnBody);
    }

    private Collider2D[] collider2DResults = new Collider2D[10];
    public void AttackCheck()
    {
        int numColliders = Physics2D.OverlapCircleNonAlloc(head.transform.position, 0.5f, collider2DResults, head.returnLayer);
        for (int i = 0; i < numColliders; i++) {
            if (collider2DResults[i].gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                collider2DResults[i].GetComponent<IDamageable>().ApplyDamage(head.player.returnDamage, head.transform);
            }
        }
    }
}
