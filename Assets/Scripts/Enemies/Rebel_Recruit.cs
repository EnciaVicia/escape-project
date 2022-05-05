using UnityEngine;
using UnityEngine.AI;

public class Rebel_Recruit : Enemy
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    public int waypointIndex = 0;
    public float rotationSpeed = 1f;
    public float minimumDistanceToWaypoint = 1f;
    public float minimumDistanceToPlayer = 10f;
    public float maxDistanceToPlayer = 40f;
    public Transform playerTransform;
    public Animator rebelAnimation;

    public RebelActivity rebelActivity;
    NavMeshAgent agent;
    void Start()
    {
      agent = GetComponent<NavMeshAgent>();
      rebelAnimation.SetBool("isIdle", false);
      rebelActivity = RebelActivity.Patrol;
      damage = 5;
      health = 100;
      lookRange = 20f;
      gunRange = 25f;
      spread = 0.1f;
    }

    void Update()
    {
      if (health <= 0) {
        rebelActivity = RebelActivity.Dead;
      }
      switch(rebelActivity)
      {
        case RebelActivity.Patrol:
         if (PlayerFound())
         {
           rebelAnimation.SetBool("isPatrolling", false);
           rebelActivity = RebelActivity.Chase;
         } 
         else 
         {
           rebelAnimation.SetBool("isPatrolling", true);
           Move();
         }
         break;
         case RebelActivity.Chase:
         if (Vector3.Distance(playerTransform.position, transform.position) >= maxDistanceToPlayer)
         {
           rebelAnimation.SetBool("isChasing", false);
           rebelActivity = RebelActivity.Patrol;
         } else
         {
           rebelAnimation.SetBool("isChasing", true);
           ChasePlayer();
         }
         break;
         case RebelActivity.Idle:
           rebelAnimation.SetBool("isPatrolling", false);
           rebelAnimation.SetBool("isIdle", true);
         break;
         case RebelActivity.Dead:
           OnDeath();
         break;
      }
    }
    public void TakeDamage(int amount)
    {
      health -= amount;
    }

    void Move()
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
     protected virtual bool PlayerFound()
  {
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRange);
    foreach (var hitCollider in hitColliders) {
      if (hitCollider.transform.tag == "Player") {
        return true;
      }
    }
    return false;
  }
    void ChasePlayer()
    {
      if (Vector3.Distance(playerTransform.position, transform.position) >= minimumDistanceToPlayer)
      {
        Vector3 distanceToPlayer = playerTransform.position - transform.position;
        agent.SetDestination(playerTransform.position * patrolSpeed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, -playerTransform.forward, rotationSpeed * Time.deltaTime);
        Shoot();
      }
    }
    void OnDeath()
    {
      Destroy(this.gameObject, 15f);
    }
}
