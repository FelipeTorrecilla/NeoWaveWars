using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    public SoldierStats soldierStats;
    public State currentState;

    private void Awake()
    {
        soldierStats = GetComponent<SoldierStats>();
    }

    private void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
