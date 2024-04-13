using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerBattleState : EnemyState
{
    private Transform player;
    private Enemy_DeathBringer enemy;
    private int moveDirection;

    public DeathBringerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        //if (enemy.isPlayerDetected().distance > enemy.attackDistance)
        //{
        //    AudioManager.instance.PlaySFX(9, enemy.transform);
        //}

        //if (player.GetComponent<PlayerStats>().isDead)
        //{
        //    stateMachine.ChangeState(enemy.moveState);
        //}
    }
    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.isPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.idleState);
                }
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDirection = -1;
        }

        if (enemy.isPlayerDetected() && enemy.isPlayerDetected().distance < enemy.attackDistance - .5f)
        {
            return;
        }

        enemy.setVelocity(enemy.moveSpeed * moveDirection, rb.velocity.y);
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
}
