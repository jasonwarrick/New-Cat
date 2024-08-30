using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_CatDestinationSetter : MonoBehaviour {
    
    [SerializeField] Transform goal;

    bool isMovingToDest = false;
    Vector3 currentDest;

    Queue<Transform> destinations = new Queue<Transform>();

    NavMeshAgent agent;
    
    void Awake () {
        agent = GetComponent<NavMeshAgent>();
        // AddDestination(goal);
    }

    public void AddDestination(Transform newDestination) {
        destinations.Enqueue(newDestination);
        Debug.Log("Added destination: " + newDestination.name);
    }

    void Update() {
        if (!isMovingToDest && destinations.Count > 0) {
            SetNextDestination();
            MoveToDestination();
        } else {
            isMovingToDest = !GetPathComplete();
        }
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
