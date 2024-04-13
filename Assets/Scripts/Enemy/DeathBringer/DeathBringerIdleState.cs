using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerIdleState : EnemyState
{
    private Transform player;

    private Enemy_DeathBringer enemy;

    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();

        //AudioManager.instance.PlaySFX(12, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        if(Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
        {
            enemy.bossFightBegin = true;
        }

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    stateMachine.ChangeState(enemy.teleportState);
        //}

        if(stateTimer < 0 && enemy.bossFightBegin)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
