using UnityEngine;

public class Idle : IState
{
    bool active = false;

    public void Enter(SC_CatBrain catBrain) {
        Debug.Log("entering idle state");
        active = true;
    }

    public void Execute(SC_CatBrain catBrain) {
        // Debug.Log("updating idle state");
    }

    public void Exit(SC_CatBrain catBrain) {
        Debug.Log("exiting idle state");
        active = false;
    }
}
