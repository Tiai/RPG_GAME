public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f;
        player.setVelocity(5 * -player.facingDirection, player.jumpForce);

        AudioManager.instance.PlaySFX(32, null);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(32);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
