using UnityEngine;
using UnityEngine.AI;

// State machine code taken from: https://discussions.unity.com/t/c-proper-state-machine/613267
public interface IState
{
    public void Enter(SC_CatBrain catBrain);
    public void Execute(SC_CatBrain catBrain);
    public void Exit(SC_CatBrain catBrain);
}

public class SC_CatBrain : MonoBehaviour
{
    // Navigation vars
    public Transform nav_goal;

    IState currentState;

    Idle idleState = new Idle();
    Walking walkingState = new Walking();

    void Awake() {
        ChangeState(idleState);
    }

    void Update() {
        if (currentState != null) { currentState.Execute(this); } // Exectute the current state if it isn't null
    }

    public void ChangeState(IState newState) {
        if (currentState != null) { currentState.Exit(this); } // Exit the current state if it isn't null

        currentState = newState;
        currentState.Enter(this);
    }

    public void MoveToMinigame(Transform newGoal) {
        nav_goal = newGoal;
        ChangeState(walkingState);
    }
}