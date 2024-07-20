using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadState 
{
    protected HeadStateMachine stateMachine;
    protected Head head;
    protected int animBoolHash;
    protected bool endTriggerCall;

    public HeadState(Head head, HeadStateMachine stateMachine, string animBoolName) {
        this.head = head;
        this.stateMachine = stateMachine;
        animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void Enter() {
        if (head.AnimatorCompo != null) {
            head.AnimatorCompo.SetBool(animBoolHash, true);
        }
        else {
            Debug.LogWarning("Animator Component does not exist.");
        }
        endTriggerCall = false;
    }

    public virtual void UpdateState() {

    }

    public virtual void Exit() {
        if (head.AnimatorCompo != null) {
            head.AnimatorCompo.SetBool(animBoolHash, false);
        }
        else {
            Debug.LogWarning("Animator Component does not exist");
        }
    }
}
