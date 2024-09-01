using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingToDest : IState
{
    Transform goal;
    bool active = false;
    bool isMovingToDest = false;
    Vector3 currentDest;

    NavMeshAgent agent;
    Queue<Transform> destinations = new Queue<Transform>();

    // State methods

    public void Enter(SC_CatBrain catBrain) {
        Debug.Log("entering walking state");
        AddDestination(catBrain.nav_goal);
        agent = catBrain.GetComponent<NavMeshAgent>();
        Debug.Log("agent state is " + (agent != null));

        active = true;

    }

    public void Execute(SC_CatBrain catBrain) {
        if (active) {
            if (!isMovingToDest && destinations.Count > 0) {
                SetNextDestination();
                MoveToDestination();
            } else {
                isMovingToDest = !GetPathComplete();
            }
        }
    }

    public void Exit(SC_CatBrain catBrain) {
        Debug.Log("exiting walking state");
        active = false;
    }

    // Cat movement methods

    public void AddDestination(Transform newDestination) {
        destinations.Enqueue(newDestination);
        Debug.Log("Added destination: " + newDestination.name);
    }

    void SetNextDestination() {
        currentDest = destinations.Dequeue().position;
        Debug.Log("Moving to first destination in queue");
    }

    void MoveToDestination() {
        agent.SetDestination(currentDest);
        isMovingToDest = true;
    }

    bool GetPathComplete() {
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }

        return false;
    }
}