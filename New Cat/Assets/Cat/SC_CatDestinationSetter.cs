using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {
    
    [SerializeField] Transform goal;

    bool readyToGo = false;

    Queue<Transform> destinations = new Queue<Transform>();

    NavMeshAgent agent;
    
    void Awake () {
        agent = GetComponent<NavMeshAgent>();
        AddDestination(goal);
        readyToGo = true;
    }

    public void AddDestination(Transform newDestination) {
        destinations.Enqueue(newDestination);
    }

    void Update() {
        if (readyToGo && destinations.Count > 0) {
            agent.SetDestination(destinations.Dequeue().position);
        }
    }
}
