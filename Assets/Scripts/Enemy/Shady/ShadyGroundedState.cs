using UnityEngine;

public class ShadyGroundedState : EnemyState
{
    protected Transform player;

    protected Enemy_Shady enemy;

    public ShadyGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Shady enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.agroDiatance /*enemy can attack while player is behind him */)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
