using UnityEngine;
using UnityEngine.AI;

public class Rebel_Recruit : Enemy
{
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    public WaypointPatrol waypointPatrolling;
    public AudioSource audioSource;
    public AudioClip gunShot;
    public float rotationSpeed = 1f;
    public float minimumDistanceToPlayer = 10f;
    public float maxDistanceToPlayer = 40f;
    public Transform playerTransform;
    public Animator rebelAnimation;
    public int fireRange;
    public bool isShooting;
    public AudioClip yell;
    bool hasYelled;

    public RebelActivity rebelActivity;
    NavMeshAgent agent;
    void Start()
    {
      waypointPatrolling = GetComponent<WaypointPatrol>();
      agent = GetComponent<NavMeshAgent>();
      rebelAnimation.SetBool("isIdle", false);
      rebelActivity = RebelActivity.Idle;
      damage = 5;
      health = 100;
      lookRange = 30f;
      gunRange = 30;
      spread = 0.001f;
      isShooting = false;
      audioSource = gameObject.AddComponent<AudioSource>();
      hasYelled = false;
    }

    void Update()
    {
      if (health <= 0) {
        rebelActivity = RebelActivity.Dead;
      }
      switch(rebelActivity)
      {
         case RebelActivity.Chase:
         if (!PlayerFound())
         {
           rebelAnimation.SetBool("isChasing", false);
           rebelAnimation.SetBool("isIdle", true);
           rebelActivity = RebelActivity.Patrol;
         } else
         {
           rebelAnimation.SetBool("isIdle", false);
           rebelAnimation.SetBool("isChasing", true);
           ChasePlayer();
         }
         break;
         case RebelActivity.Attack:
          rebelAnimation.SetBool("isChasing", false);
          rebelAnimation.SetBool("isFiring", true);
           if (isPlayerInRange()) {
            rebelActivity = RebelActivity.Chase;
         }
         if (!isShooting) {
           isShooting = true;
           Invoke("Attack", 1.2f);
         }
         break;
         case RebelActivity.Idle:
           if (PlayerFound())
           {
              rebelAnimation.SetBool("isIdle", false);
              rebelAnimation.SetBool("isChasing", true);
              rebelActivity = RebelActivity.Chase;
           } 
           else 
           {
             rebelAnimation.SetBool("isIdle", true);
             rebelAnimation.SetBool("isChasing", false);
           }
         break;
         case RebelActivity.Dead:
         rebelAnimation.SetBool("isFiring", false);
         rebelAnimation.SetBool("isChasing", false);
         rebelAnimation.SetBool("isDead", true);
           OnDeath();
         break;
      }
    }
    public void TakeDamage(int amount)
    {
      health -= amount;
    }
     protected virtual bool PlayerFound()
  {
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRange);
    foreach (var hitCollider in hitColliders) {
      if (hitCollider.transform.tag == "Player") {
        if (!hasYelled) {
          audioSource.clip = yell;
          audioSource.Play();
          hasYelled = true;
        }
        return true;
      }
    }
    return false;
  }
    void ChasePlayer()
    {
      if (!PlayerFound()) {
        rebelActivity = RebelActivity.Patrol;
        return;
      }
      if (!isPlayerInRange())
      {
        agent.SetDestination(playerTransform.position);
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
      } else {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        rebelActivity = RebelActivity.Attack;
        agent.SetDestination(transform.position);
      }
    }
    void OnDeath()
    {
      
      Destroy(this.gameObject, 15f);
    }
    bool isPlayerInRange()
    {
      Collider[] playerInFireRange = Physics.OverlapSphere(transform.position, fireRange);
      foreach (Collider c in playerInFireRange)
      {
        if (c.transform.tag == "Player") {
          return true;
        }
      }
        return false;
    }
    void Attack()
    {
      audioSource.clip = gunShot;
      audioSource.Play();
      Shoot();
      isShooting = false;
    }
}
