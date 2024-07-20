using System;
using System.Collections;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;

    public IMovement MovementCompo {get; protected set;}
    public SpriteRenderer SpriteRendererCompo {get; protected set;}
    public Animator AnimatorCompo {get; protected set;}
    public Rigidbody2D RigidbodyCompo {get; protected set;}
    public Health HealthCompo {get; protected set;}

    public int FacingDirection { get; protected set; } = 1;
    public bool CanStateChangeable {get; private set;} = true;

    protected virtual void Awake() {
        if(TryGetComponent(out Animator animator))
            AnimatorCompo = GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();
        if(TryGetComponent(out IMovement movement)) {
            MovementCompo = movement;
            MovementCompo.Initialize(this);
        }
        if (TryGetComponent(out Health health)) {
            HealthCompo = health;
            HealthCompo.SetOwner(this);
        }
    }

    #region Delay callback coroutine
    public Coroutine StartDelayCallback(float delayTime, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayTime, Callback));
    }

    public Coroutine StartDelayCallback(Func<bool> delayAction, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayAction, Callback));
    }

    protected IEnumerator DelayCoroutine(float delayTime, Action Callback)
    {
        yield return new WaitForSeconds(delayTime);
        Callback?.Invoke();
    }

    protected IEnumerator DelayCoroutine(Func<bool> delayAction, Action callback)
    {
        yield return new WaitUntil(() => delayAction.Invoke());
        callback?.Invoke();
    }
    #endregion

    #region VelocityControl

    public void SetVelocity(float x, float y, bool doNotFlip = false) {
        RigidbodyCompo.velocity = new Vector2(x, y);

        if(!doNotFlip) FlipController(x);
    }

    public void StopImmediately(bool withYAxis) {
        if(withYAxis) RigidbodyCompo.velocity = Vector2.zero;
        else RigidbodyCompo.velocity = new Vector2(0, RigidbodyCompo.velocity.y);
    }

    #endregion

    #region FlipController

    public virtual void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0, 180f, 0);
    }

    public virtual void FlipController(float x) {
        if(Mathf.Abs(x) < 0.05f) return;
        
        if(Mathf.Abs(FacingDirection + Mathf.Sign(x)) < 0.5f)
            Flip();
    }

    #endregion
}
