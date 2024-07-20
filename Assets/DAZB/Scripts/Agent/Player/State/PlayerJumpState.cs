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
        if (Keyboard.current.sKey.isPressed) {
            player.gameObject.layer = LayerMask.NameToLayer("PlayerUnderJump");
            player.StartDelayCallback(0.5f, () => {
                player.gameObject.layer = LayerMask.NameToLayer("Player");
                stateMachine.ChangeState(PlayerStateEnum.Idle);
            });
        }
        else
        {
            currentJumpPower = player.limitJumpPower / 2;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.MovementCompo.SetMovement(player.InputReader.Movement * player.moveSpeed);
        if (Keyboard.current.sKey.isPressed) {
            currentJumpPower = 0;
        }
        if (Keyboard.current.spaceKey.isPressed) {
            currentJumpPower += 0.5f;
            player.RigidbodyCompo.velocity = new Vector2(player.RigidbodyCompo.velocity.x, 0);
            player.RigidbodyCompo.AddForce(Vector2.up * currentJumpPower, ForceMode2D.Impulse);
            if (currentJumpPower >= player.limitJumpPower)
            {
                stateMachine.ChangeState(PlayerStateEnum.Idle);
            }
        }
    }
}
