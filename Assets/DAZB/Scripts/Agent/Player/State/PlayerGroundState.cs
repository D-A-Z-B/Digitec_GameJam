using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputReader.JumpEvent += HandleJumpEvent;
    }

    public override void Exit()
    {
        player.InputReader.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }

    private void HandleJumpEvent() {
        if (!(player.MovementCompo as AgentMovement).IsGround) return;
        stateMachine.ChangeState(PlayerStateEnum.Jump);
    }
}