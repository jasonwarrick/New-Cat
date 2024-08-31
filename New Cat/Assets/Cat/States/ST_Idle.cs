using UnityEngine;

public class Idle : IState
{
    public void Enter(SC_CatBrain catBrain) {
        Debug.Log("entering idle state");
    }

    public void Execute(SC_CatBrain catBrain) {
        Debug.Log("updating idle state");
    }

    public void Exit(SC_CatBrain catBrain) {
        Debug.Log("exiting idle state");
    }
}
