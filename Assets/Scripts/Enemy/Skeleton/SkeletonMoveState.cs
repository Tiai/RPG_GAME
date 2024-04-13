using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(34, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(34);
    }

    public override void Update()
    {
        base.Update();

        enemy.setVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);

        if(enemy.isWallDetected() || !enemy.isGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
