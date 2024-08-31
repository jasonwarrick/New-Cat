using UnityEngine;

public class Walking : IState
{
    public void Enter(SC_CatBrain catBrain) {
        Debug.Log("entering walking state");
    }

    public void Execute(SC_CatBrain catBrain) {
        Debug.Log("updating walking state");
    }

    public void Exit(SC_CatBrain catBrain) {
        Debug.Log("exiting walking state");
    }
}
