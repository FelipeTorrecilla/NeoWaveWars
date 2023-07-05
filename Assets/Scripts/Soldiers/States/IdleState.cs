using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeEnemy;

    public override State RunCurrentState()
    {
        if (canSeeEnemy)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
