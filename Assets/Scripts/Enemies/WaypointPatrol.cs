using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public int waypointIndex = 0;
    [SerializeField] private Transform[] waypoints;
    public float minimumDistanceToWaypoint = 1f;
    void Start()
    {
        
    }

    public void Patrol(float patrolSpeed, NavMeshAgent agent, float rotationSpeed)
    {
       // Vector representando la distancia entre el waypoint y el enemigo
      Vector3 distanceToWaypoint = waypoints[waypointIndex].position - transform.position;
      // Vector representando la direccion del enemigo en relacion al waypoint;
      Vector3 waypointDirection = distanceToWaypoint.normalized;
      // con esto el enemigo avanza hacia el waypoint
      agent.SetDestination(waypointDirection * patrolSpeed * Time.deltaTime);
      Debug.Log(agent.destination);
      // con esto el enemigo se gira suavemente hacia el waypoint
      transform.forward = Vector3.Lerp(transform.forward, waypointDirection, rotationSpeed * Time.deltaTime);

      // Si el enemigo llega al waypoint..
      if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) <= minimumDistanceToWaypoint)
      {
        // Si el indice actual es mayor o igual al total de waypoints
        if (waypointIndex >= waypoints.Length - 1)
        {
          // se reinicia el waypoint a seguir
          waypointIndex = 0;
        } else
        {
          // la direccion cambia al siguiente waypoint
          waypointIndex++;
        }
      }
    }
}
