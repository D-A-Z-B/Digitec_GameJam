using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour, IMovement
{
    private Vector2 velocity;
    public Vector2 Velocity => velocity;
    private Head agent;

    public void Initialize(Agent agent)
    {
        this.agent = agent as Head;
    }

/*     private void FixedUpdate() {
        Move();
    }

    private void Move()
    {
        agent.RigidCompo.velocity = velocity;
    }
 */
    public void SetMovement(Vector3 movement)
    {
        velocity = movement;
    }

    public void StopImmediately()
    {
        velocity = Vector2.zero;
    }
}
