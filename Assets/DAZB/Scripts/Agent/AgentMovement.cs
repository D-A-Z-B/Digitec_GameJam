using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType {
    XY,
    X
}

public class AgentMovement : MonoBehaviour, IMovement
{
    [Header("Ground Check")]
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private MovementType movementType;
    private Player agent;

    private Vector2 velocity;
    public Vector2 Velocity => velocity;
    public bool IsGround => IsGroundMethod();

    public void Initialize(Agent agent) {
        this.agent = agent as Player;
    }

    private void FixedUpdate() {
        Move();
        Filp();
    }

    public void SetMovement(Vector3 movement) {
        velocity = movement;
    }

    private void Filp() {
        if (agent.InputReader.Movement.x > 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (agent.InputReader.Movement.x < 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void StopImmediately() {
        velocity = Vector2.zero;
    }

    private void Move() {
        switch (movementType) {
            case MovementType.XY:
                agent.RigidCompo.velocity = new Vector3(velocity.x, velocity.y);
            break;
            case MovementType.X:
                agent.RigidCompo.velocity = new Vector3(velocity.x, agent.RigidCompo.velocity.y);
            break;
        }
    }

    private bool IsGroundMethod() {
        if (Physics2D.OverlapBox(transform.position + offset, groundCheckSize, 0, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, groundCheckSize);
    }
}
