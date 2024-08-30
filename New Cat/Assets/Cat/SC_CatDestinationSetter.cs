using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {
    
    [SerializeField] Transform goal;
    
    void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position; 
    }
}
