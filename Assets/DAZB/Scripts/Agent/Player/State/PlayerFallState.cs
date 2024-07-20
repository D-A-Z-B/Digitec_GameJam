using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if ((player.MovementCompo as AgentMovement).IsGround) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}