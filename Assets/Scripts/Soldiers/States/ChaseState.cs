using UnityEngine;

public class ChaseState : State
{
    public IdleState idleState;
    public AttackState attackState;
    public bool isInAttackRange;

    public override State RunCurrentState()
    {
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}

