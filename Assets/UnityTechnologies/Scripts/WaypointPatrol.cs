using UnityEngine;
using UnityEngine.AI;
public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public Transform player;
    public bool m_isInRange = false;
    int m_CurrentWaypointIndex;
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_isInRange = true;
            player = other.transform;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_isInRange = false;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }


    void Update()
    {
        if (m_isInRange)
        {
            navMeshAgent.SetDestination(player.position);
        } else if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
