using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArcherBattleState : EnemyState
{
    private Enemy_Archer enemy;

    private Transform player;

    private int moveDirection;

    public ArcherBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Archer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (enemy.isPlayerDetected().distance > enemy.attackDistance)
        {
            //AudioManager.instance.PlaySFX(9, enemy.transform);
        }

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.isPlayerDetected().distance < enemy.safeDistance)
            {
                if (CanJump())
                {
                    stateMachine.ChangeState(enemy.jumpState);
                }
                //else
                //{
                //    stateMachine.ChangeState(enemy.closeAttackState)
                //}
            }

            if (enemy.isPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        BattleStateFlipController();

    }

    private void BattleStateFlipController()
    {
        if (player.position.x > enemy.transform.position.x && enemy.facingDirection == -1)
        {
            enemy.Flip();
        }
        else if (player.position.x < enemy.transform.position.x && enemy.facingDirection == 1)
        {
            enemy.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();

        //AudioManager.instance.StopSFX(9);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastAttackTime = Time.time;
            return true;
        }
        return false;
    }

    private bool CanJump()
    {
        if(enemy.GroundBehindCheck() == false || enemy.WallBehindCheck() == true)
        {
            return false;
        }

        if(Time.time >= enemy.lastTimeJump + enemy.jumpCooldown)
        {
            enemy.lastTimeJump = Time.time;
            return true;
        }
        return false;
    }
}
