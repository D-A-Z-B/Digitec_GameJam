using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    float currentJumpPower = 0;

    public override void Enter()
    {
        base.Enter();
        currentJumpPower = player.limitJumpPower / 2;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.MovementCompo.SetMovement(player.InputReader.Movement * player.moveSpeed);
        if (Keyboard.current.spaceKey.isPressed) {
            currentJumpPower += 0.5f;
            player.RigidCompo.velocity = new Vector2(player.RigidCompo.velocity.x, 0);
            player.RigidCompo.AddForce(Vector2.up * currentJumpPower, ForceMode2D.Impulse);
            if (currentJumpPower >= player.limitJumpPower) {
                stateMachine.ChangeState(PlayerStateEnum.Idle);
            }
        } else {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
