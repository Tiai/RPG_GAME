using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.stats.MakeInvincible(true);

        AudioManager.instance.PlaySFX(14, null);

        player.skill.dash.CloneOnDash();

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(14);

        player.skill.dash.CloneOnArrival();
        player.setVelocity(0, rb.velocity.y);

        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if(player.isGroundDetected() && player.isWallDetected() )
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        player.setVelocity(player.dashSpeed * player.dashDirection, 0); // if y isn't set to zero, player won't drop while dash.

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.fx.CreateMirageImage();
    }
}
