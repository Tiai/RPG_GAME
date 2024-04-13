using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _StatMachine, string _animBoolName) : base(_player, _StatMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(18, null);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(18);
    }

    public override void Update()
    {
        base.Update();

        player.setVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0 || player.isWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}